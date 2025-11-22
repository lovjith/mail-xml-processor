namespace MailXmlProcessor.Domain.Entities;

public class XmlBlock
{
    public string Content { get; }

    public XmlBlock(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("XML content cannot be empty.");

        Content = content;
    }
}
