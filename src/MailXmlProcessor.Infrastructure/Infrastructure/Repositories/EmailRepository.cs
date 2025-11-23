using MailXmlProcessor.Application.Interfaces;
using MailXmlProcessor.Domain.Entities;
using MailXmlProcessor.Infrastructure.Persistence;
using MailXmlProcessor.Infrastructure.Persistence.Entities;

namespace MailXmlProcessor.Infrastructure.Repositories;

public class EmailRepository(MailXmlDbContext db) : IEmailRepository
{
    private readonly MailXmlDbContext _db = db;

    public async Task SaveAsync(List<ExtractedEmail> emails)
    {
        foreach (var email in emails)
        {
            var emailRecord = new EmailRecord
            {
                EmailIndex = email.Index,
                // RawEmail = "" // store raw text later
            };

            _db.Emails.Add(emailRecord);
            await _db.SaveChangesAsync();

            foreach (var xml in email.XmlBlocks)
            {
                _db.XmlBlocks.Add(new XmlBlockRecord
                {
                    EmailRecordId = emailRecord.Id,
                    Content = xml.Content
                });
            }

            foreach (var field in email.TaggedFields)
            {
                _db.TaggedFields.Add(new TaggedFieldRecord
                {
                    EmailRecordId = emailRecord.Id,
                    Tag = field.Tag,
                    Value = field.Value
                });
            }
        }

        await _db.SaveChangesAsync();
    }
}
