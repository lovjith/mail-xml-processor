namespace MailXmlProcessor.Application.Models;

public class ExtractionResult
{
    public int EmailIndex { get; set; }
    public List<string> ExtractedXmlBlocks { get; set; } = [];
    public List<Dictionary<string, string>> ExtractedJsonBlocks { get; set; } = [];
    public Dictionary<string, string> TaggedFields { get; set; } = [];
    public List<string> Errors { get; set; } = [];
}
