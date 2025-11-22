using MailXmlProcessor.Domain.Entities;
using MailXmlProcessor.Domain.Services;
using System.Text.RegularExpressions;

namespace MailXmlProcessor.Infrastructure.Services;

public class EmailExtractionService : IEmailParser
{
    public List<ExtractedEmail> Parse(List<string> emails)
    {
        var results = new List<ExtractedEmail>();

        for (int i = 0; i < emails.Count; i++)
        {
            var emailText = emails[i];

            var xmlBlocks = new List<XmlBlock>();
            var taggedFields = new List<TaggedField>();
            var errors = new List<ExtractionError>();

            try
            {
                // Detect broken XML islands
                var islandMatches = Regex.Matches(emailText, @"<(\w+)>[\s\S]*?</\1>");
                if (islandMatches.Count == 0 && emailText.Contains("<expense"))
                {
                    errors.Add(new ExtractionError(
                        "Malformed XML island detected",
                        "<expense...>"
                    ));
                }

                foreach (Match match in islandMatches)
                {
                    xmlBlocks.Add(new XmlBlock(match.Value));
                }

                // Detect broken tags
                var openTags = Regex.Matches(emailText, @"<\w+>");
                var closeTags = Regex.Matches(emailText, @"</\w+>");

                if (openTags.Count != closeTags.Count)
                {
                    errors.Add(new ExtractionError(
                        "Mismatched opening and closing tags"
                    ));
                }

                // Extract valid tagged fields
                var taggedMatches = Regex.Matches(emailText, @"<(\w+)>(.*?)</\1>");
                foreach (Match match in taggedMatches)
                {
                    taggedFields.Add(
                        new TaggedField(
                            match.Groups[1].Value,
                            match.Groups[2].Value.Trim()
                        )
                    );
                }
            }
            catch (Exception ex)
            {
                errors.Add(new ExtractionError("Unhandled parsing exception", ex.Message));
            }

            results.Add(
                new ExtractedEmail(
                    i,
                    xmlBlocks,
                    taggedFields,
                    errors
                )
            );
        }

        return results;
    }
}
