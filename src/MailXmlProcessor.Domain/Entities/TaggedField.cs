namespace MailXmlProcessor.Domain.Entities;
public class TaggedField
{
    public string Tag { get; }
    public string Value { get; }

    public TaggedField(string tag, string value)
    {
        if (string.IsNullOrWhiteSpace(tag))
            throw new ArgumentException("Tag cannot be empty.");

        Tag = tag;
        Value = value;
    }
}
