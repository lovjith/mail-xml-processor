using MailXmlProcessor.Application.Models;

namespace MailXmlProcessor.Application.Interfaces;

public interface IEmailExtractionService
{
    List<ExtractionResult> Extract(List<string> emails);
}