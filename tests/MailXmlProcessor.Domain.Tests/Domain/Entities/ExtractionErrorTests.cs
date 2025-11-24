using MailXmlProcessor.Domain.Entities;

namespace MailXmlProcessor.Domain.Tests.Entities;

public class ExtractionErrorTests
{
    [Fact]
    public void Should_Create_Error_With_Message()
    {
        var error = new ExtractionError("Invalid XML");

        Assert.Equal("Invalid XML", error.Message);
    }
}
