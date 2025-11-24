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

    [HttpPost("process")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public ActionResult<List<ExtractionResult>> Process([FromBody] EmailBatchRequest request)
    {
        if (request == null)
            return BadRequest("Request body is required.");

        if (request.Emails == null || request.Emails.Count == 0)
            return BadRequest("No emails provided.");

        var result = _service.Extract(request.Emails);

        return Ok(result);
    }
}
