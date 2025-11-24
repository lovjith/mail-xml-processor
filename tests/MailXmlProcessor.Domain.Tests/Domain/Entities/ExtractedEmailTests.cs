using MailXmlProcessor.Domain.Entities;

namespace MailXmlProcessor.Domain.Tests.Entities;

public class ExtractedEmailTests
{
    [Fact]
    public void Should_Create_Email_With_Proper_Index()
    {
        var email = new ExtractedEmail(
            1,
            [],
            [],
            [],
            []
        );

        Assert.Equal(1, email.Index);
    }
}
