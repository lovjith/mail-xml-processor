namespace MailXmlProcessor.Domain.Entities;

public class ExtractedEmail
{
    public int Index { get; }
    public IReadOnlyList<XmlBlock> XmlBlocks { get; }
    public IReadOnlyList<TaggedField> TaggedFields { get; }
    public IReadOnlyList<ExtractionError> Errors { get; }

    public bool HasErrors => Errors.Any();

    public ExtractedEmail(
        int index,
        List<XmlBlock> xmlBlocks,
        List<TaggedField> taggedFields,
        List<ExtractionError> errors)
    {
        Index = index;
        XmlBlocks = xmlBlocks;
        TaggedFields = taggedFields;
        Errors = errors;
    }
}
