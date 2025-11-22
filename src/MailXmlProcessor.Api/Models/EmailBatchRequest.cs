namespace MailXmlProcessor.Application.Models;

public class EmailBatchRequest
{
    public List<string> Emails { get; set; } = new();
}
