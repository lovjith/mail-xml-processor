using MailXmlProcessor.Domain.Entities;

namespace MailXmlProcessor.Application.Extensions;

public static class ExtractedEmailExtensions
{
    public static void ApplyTaxCalculation(this ExtractedEmail email, float salesTaxRate)
    {
        var totalField = email.TaggedFields
            .FirstOrDefault(t => t.Tag.Equals("total", StringComparison.OrdinalIgnoreCase));

        var total = email.Data?.Total;

        if (total == null)
            return;

        var salesTax = total.Value * (decimal)(salesTaxRate / (1 + salesTaxRate));
        var gross = total.Value - salesTax;

        if (email.Data != null)
        {
            email.Data.SalesTax = Math.Round(salesTax, 2);
            email.Data.TotalExcludingTax = Math.Round(gross, 2);
        }
    }
}
