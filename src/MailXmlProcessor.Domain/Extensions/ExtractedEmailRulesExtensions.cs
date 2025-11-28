using MailXmlProcessor.Domain.Entities;
using MailXmlProcessor.Domain.Models;

namespace MailXmlProcessor.Domain.Services;

public static class EmailExtractionRulesExtensions
{
    public static void ApplyExtractionRules(this ExtractedEmail email)
    {
        // Rule 1: Mismatched tags → reject
        var hasMalformedError = email.Errors
            .Any(e => e.Message.Contains("Malformed") ||
                      e.Message.Contains("Mismatched"));

        if (hasMalformedError)
        {
            email.Errors.Add(new ExtractionError(
                "Message rejected due to malformed tags"
            ));
            return;
        }

        var hasTotal = email.JsonBlocks.Any(b => b.ContainsKey("total"));

        if (!hasTotal)
        {
            email.Errors.Add(new ExtractionError("Missing <total> - message rejected"));
            return;
        }

        var hasCostCentre = email.JsonBlocks.Any(b => b.ContainsKey("cost_centre"));

        if (!hasCostCentre)
        {
            email.Data ??= new ExpenseData();
            email.Data.CostCentre = "UNKNOWN";
        }
    }
}
