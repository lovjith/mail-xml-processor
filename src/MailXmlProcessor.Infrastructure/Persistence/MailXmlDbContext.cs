using Microsoft.EntityFrameworkCore;
using MailXmlProcessor.Infrastructure.Persistence.Entities;

namespace MailXmlProcessor.Infrastructure.Persistence
{
    public class MailXmlDbContext(DbContextOptions<MailXmlDbContext> options) : DbContext(options)
    {
        public DbSet<EmailRecord> Emails => Set<EmailRecord>();
        public DbSet<XmlBlockRecord> XmlBlocks => Set<XmlBlockRecord>();
        public DbSet<TaggedFieldRecord> TaggedFields => Set<TaggedFieldRecord>();
    }
}
