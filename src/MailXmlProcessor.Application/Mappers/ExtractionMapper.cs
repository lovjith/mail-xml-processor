using MailXmlProcessor.Application.Models;
using MailXmlProcessor.Domain.Entities;

namespace MailXmlProcessor.Application.Mappers;

public static class ExtractionMapper
{
    public static ExtractionResult Map(ExtractedEmail email)
    {
        return new ExtractionResult
        {
            EmailIndex = email.Index,
            ExtractedXmlBlocks = [.. email.XmlBlocks.Select(x => x.Content)],
            ExtractedJsonBlocks = email.JsonBlocks,
            TaggedFields = email.TaggedFields.ToDictionary(t => t.Tag, t => t.Value),
            Errors = email.Errors.Select(e => e.Message).ToList()
        };
    }
}
