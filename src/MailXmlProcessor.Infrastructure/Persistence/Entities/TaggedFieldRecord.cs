namespace MailXmlProcessor.Infrastructure.Persistence.Entities;

public class TaggedFieldRecord
{
    public int Id { get; set; }
    public int EmailRecordId { get; set; }
    public string Tag { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
