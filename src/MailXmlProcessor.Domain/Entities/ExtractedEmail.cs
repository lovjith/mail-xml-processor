using MailXmlProcessor.Domain.Models;

namespace MailXmlProcessor.Domain.Entities;

public class ExtractedEmail(
    int index,
    List<XmlBlock> xmlBlocks,
    List<Dictionary<string, string>> jsonBlocks,
    List<TaggedField> taggedFields,
    List<ExtractionError> errors)
{
    public int Index { get; } = index;
    public ExpenseData? Data { get; set; }
    public List<XmlBlock> XmlBlocks { get; } = xmlBlocks;
    public List<Dictionary<string, string>> JsonBlocks { get; } = jsonBlocks;
    public List<TaggedField> TaggedFields { get; } = taggedFields;
    public List<ExtractionError> Errors { get; } = errors;
}
