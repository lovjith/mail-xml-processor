using MailXmlProcessor.Domain.Entities;

namespace MailXmlProcessor.Domain.Tests.Entities;

public class XmlBlockTests
{
    [Fact]
    public void Should_Store_Xml_Content()
    {
        var xml = "<expense><total>100</total></expense>";

        var block = new XmlBlock(xml);

        Assert.Equal(xml, block.Content);
    }

    [Fact]
    public void Should_Throw_When_Xml_Is_Empty()
    {
        Assert.Throws<ArgumentException>(() =>
            new XmlBlock(string.Empty));
    }
}
