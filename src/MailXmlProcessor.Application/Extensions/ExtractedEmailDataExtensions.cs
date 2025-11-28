using MailXmlProcessor.Domain.Entities;
using MailXmlProcessor.Domain.Models;

namespace MailXmlProcessor.Application.Extensions;

public static class ExtractedEmailDataExtensions
{
    public static void BuildExpenseData(this ExtractedEmail email)
    {
        if (email.JsonBlocks == null || email.JsonBlocks.Count == 0)
            return;

        var block = email.JsonBlocks.FirstOrDefault();
        if (block == null)
            return;

        var data = new ExpenseData();

        if (block.TryGetValue("cost_centre", out var cc))
            data.CostCentre = cc;

        if (block.TryGetValue("total", out var totalRaw) &&
            decimal.TryParse(totalRaw.Replace(",", ""), out var total))
            data.Total = total;

        if (block.TryGetValue("payment_method", out var pm))
            data.PaymentMethod = pm;

        email.Data = data;
    }
}
