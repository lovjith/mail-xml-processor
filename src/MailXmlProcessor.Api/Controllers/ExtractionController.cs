using MailXmlProcessor.Application.Interfaces;
using MailXmlProcessor.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace MailXmlProcessor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExtractionController : ControllerBase
{
    private readonly IEmailExtractionService _service;

    public ExtractionController(IEmailExtractionService service)
    {
        _service = service;
    }

    /// <summary>
    /// Process multiple email bodies and extract structured XML and tagged data.
    /// </summary>
    /// <param name="request">Batch of email bodies.</param>
    /// <returns>Structured extraction results.</returns>
    [HttpPost("process")]
    [ProducesResponseType(typeof(List<ExtractionResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Process([FromBody] EmailBatchRequest request)
    {
        if (request == null)
            return BadRequest("Request body is required.");

        if (request.Emails == null || request.Emails.Count == 0)
            return BadRequest("No emails provided.");

        var result = _service.Extract(request.Emails);

        return Ok(result);
    }
}
