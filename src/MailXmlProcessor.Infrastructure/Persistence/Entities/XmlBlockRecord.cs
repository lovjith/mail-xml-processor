namespace MailXmlProcessor.Infrastructure.Persistence.Entities;

public class XmlBlockRecord
{
    public int Id { get; set; }
    public int EmailRecordId { get; set; }
    public string Content { get; set; } = string.Empty;
}
