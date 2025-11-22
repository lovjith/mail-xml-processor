using MailXmlProcessor.Domain.Entities;

namespace MailXmlProcessor.Domain.Services;
public interface IEmailParser
{
    List<ExtractedEmail> Parse(List<string> emails);
}

