using MailXmlProcessor.Domain.Entities;
using MailXmlProcessor.Domain.Services;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.Extensions.Options;
using MailXmlProcessor.Infrastructure.Configuration;
using MailXmlProcessor.Application.Extensions;

namespace MailXmlProcessor.Infrastructure.Services;

public class EmailExtractionService(IOptions<ExtractionSettings> options) : IEmailParser
{
    private readonly ExtractionSettings _settings = options.Value;

    public List<ExtractedEmail> Parse(List<string> emails)
    {
        var results = new List<ExtractedEmail>();

        foreach (string email in emails)
        {
            var threadParts = SplitEmailThread(email);

            foreach (var emailText in threadParts)
            {
                var xmlBlocks = new List<XmlBlock>();
                var jsonBlocks = new List<Dictionary<string, string>>();
                var taggedFields = new List<TaggedField>();
                var errors = new List<ExtractionError>();

                try
                {
                    var configuredTags = _settings.XmlBlockTags;

                    var pattern = $"<({string.Join("|", configuredTags)})>[\\s\\S]*?</\\1>";

                    var islandMatches = Regex.Matches(
                        emailText,
                        pattern,
                        RegexOptions.IgnoreCase
                    );

                    if (islandMatches.Count == 0 && emailText.Contains("<expense"))
                    {
                        errors.Add(new ExtractionError(
                            "Malformed XML island detected",
                            "<expense...>"
                        ));
                    }

                    // 1. Extract XML + JSON blocks
                    foreach (Match match in islandMatches)
                    {
                        var xml = match.Value;

                        xmlBlocks.Add(new XmlBlock(xml));
                        jsonBlocks.Add(ConvertXmlToDictionary(xml));
                    }

                    // 2. Remove XML blocks before scanning for tagged fields
                    var cleanedText = emailText;

                    foreach (var block in xmlBlocks)
                    {
                        cleanedText = cleanedText.Replace(block.Content, string.Empty);
                    }

                    // 3. Find standalone tagged fields
                    var taggedMatches = Regex.Matches(cleanedText, @"<(\w+)>(.*?)</\1>");

                    foreach (Match match in taggedMatches)
                    {
                        var tag = match.Groups[1].Value;
                        var value = match.Groups[2].Value.Trim();

                        taggedFields.Add(new TaggedField(tag, value));
                    }

                }
                catch (Exception ex)
                {
                    errors.Add(new ExtractionError("Unhandled parsing exception", ex.Message));
                }

                var extracted = new ExtractedEmail(
                    results.Count,
                    xmlBlocks,
                    jsonBlocks,
                    taggedFields,
                    errors
                );

                extracted.BuildExpenseData();
                extracted.ApplyExtractionRules();
                extracted.ApplyTaxCalculation(_settings.SalesTaxRate);

                results.Add(extracted);          
            }
        }

        return results;
    }

    private static Dictionary<string, string> ConvertXmlToDictionary(string xml)
    {
        try
        {
            var doc = XDocument.Parse(xml);

            return doc
                .Descendants()
                .Where(x => !x.HasElements)
                .ToDictionary(
                    x => x.Name.LocalName,
                    x => x.Value.Trim()
                );
        }
        catch
        {
            return new Dictionary<string, string>();
        }
    }

    private static List<string> SplitEmailThread(string text)
    {
        var separators = new[]
        {
            "\nFrom:",
            "\r\nFrom:",
            "\nSent:",
            "\r\nSent:"
        };

        var parts = new List<string> { text };

        foreach (var sep in separators)
        {
            parts = [.. parts.SelectMany(part => part.Split(sep, StringSplitOptions.RemoveEmptyEntries))];
        }

        return [.. parts
            .Select(part => part.Trim())
            .Where(p =>
                !string.IsNullOrWhiteSpace(p) &&     // remove empty line
                p.Length > 30 &&                     // remove blocks less than 30 charracter
                Regex.IsMatch(p, @"[a-zA-Z0-9]")     // must contain real characters
            )];
    }

}
