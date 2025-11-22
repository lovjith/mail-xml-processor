namespace MailXmlProcessor.Application.Models;

public class ExtractionResult
{
    public int EmailIndex { get; set; }

    public List<string> ExtractedXmlBlocks { get; set; } = new();

    public Dictionary<string, string> TaggedFields { get; set; } = new();

    public List<string> Errors { get; set; } = new();
}
