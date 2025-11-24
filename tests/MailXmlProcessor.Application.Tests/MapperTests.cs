using MailXmlProcessor.Application.Mappers;
using MailXmlProcessor.Domain.Entities;

namespace MailXmlProcessor.Application.Tests;

public class MapperTests
{
    [Fact]
    public void Mapper_Should_Map_XmlBlocks()
    {
        var email = new ExtractedEmail(0, [], [], [], []);
        var result = ExtractionMapper.Map(email);

        Assert.NotNull(result.ExtractedXmlBlocks);
    }
}
