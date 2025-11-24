using MailXmlProcessor.Domain.Entities;

namespace MailXmlProcessor.Domain.Tests;

public class EmailTests
{
    [Fact]
    public void TaggedField_ShouldStoreTagAndValue()
    {
        var field = new TaggedField("vendor", "Test");

        Assert.Equal("vendor", field.Tag);
        Assert.Equal("Test", field.Value);
    }
}
