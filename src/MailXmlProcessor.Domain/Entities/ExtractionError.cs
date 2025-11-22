namespace MailXmlProcessor.Domain.Entities;

public class ExtractionError
{
    public string Message { get; }
    public string? OffendingContent { get; }

    public ExtractionError(string message, string? offendingContent = null)
    {
        Message = message;
        OffendingContent = offendingContent;
    }
}
