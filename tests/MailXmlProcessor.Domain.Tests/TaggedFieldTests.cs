using MailXmlProcessor.Domain.Entities;

namespace MailXmlProcessor.Domain.Tests.Entities;

public class TaggedFieldTests
{
    [Fact]
    public void Should_Create_Valid_TaggedField()
    {
        var field = new TaggedField("vendor", "Seaside Steakhouse");

        Assert.Equal("vendor", field.Tag);
        Assert.Equal("Seaside Steakhouse", field.Value);
    }

    [Fact]
    public void Should_Throw_When_Tag_Is_Empty()
    {
        Assert.Throws<ArgumentException>(() =>
            new TaggedField(string.Empty, "Value"));
    }

    [Fact]
    public void Should_Throw_When_Value_Is_Null()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new TaggedField("tag", null!));
    }
}
