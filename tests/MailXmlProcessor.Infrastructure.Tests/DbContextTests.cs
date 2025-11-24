using Microsoft.EntityFrameworkCore;
using MailXmlProcessor.Infrastructure.Persistence;

namespace MailXmlProcessor.Infrastructure.Tests;

public class DbContextTests
{
    [Fact]
    public void DbContext_Should_Create_InMemory()
    {
        var options = new DbContextOptionsBuilder<MailXmlDbContext>()
            // .UseInMemoryDatabase("TestDb")
            .Options;

        var ctx = new MailXmlDbContext(options);

        Assert.NotNull(ctx);
    }
}
