using MailXmlProcessor.Application.Interfaces;
using MailXmlProcessor.Application.Mappers;
using MailXmlProcessor.Application.Models;
using MailXmlProcessor.Domain.Services;

namespace MailXmlProcessor.Application.Services;

public class EmailProcessingService : IEmailExtractionService
{
    private readonly IEmailParser _parser;
    private readonly IEmailRepository _repository;
    public EmailProcessingService(IEmailParser parser, IEmailRepository repository)
    {
        _parser = parser;
        _repository = repository;
    }

    public List<ExtractionResult> Extract(List<string> emails)
    {
        var domainResult = _parser.Parse(emails);

        // TODO: Save to repository
        // _repository.SaveAsync(domainResult).Wait();
        
        return domainResult
            .Select(ExtractionMapper.Map)
            .ToList();
    }
}
