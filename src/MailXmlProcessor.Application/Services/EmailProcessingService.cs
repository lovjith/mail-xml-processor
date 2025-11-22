using MailXmlProcessor.Application.Interfaces;
using MailXmlProcessor.Application.Mappers;
using MailXmlProcessor.Application.Models;
using MailXmlProcessor.Domain.Services;

namespace MailXmlProcessor.Application.Services;

public class EmailProcessingService : IEmailExtractionService
{
    private readonly IEmailParser _parser;

    public EmailProcessingService(IEmailParser parser)
    {
        _parser = parser;
    }

    public List<ExtractionResult> Extract(List<string> emails)
    {
        var domainResult = _parser.Parse(emails);

        return domainResult
            .Select(ExtractionMapper.Map)
            .ToList();
    }
}
