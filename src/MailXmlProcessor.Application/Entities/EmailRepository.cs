using MailXmlProcessor.Domain.Entities;

namespace MailXmlProcessor.Application.Interfaces;

public interface IEmailRepository
{
    Task SaveAsync(List<ExtractedEmail> emails);
}
