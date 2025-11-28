namespace MailXmlProcessor.Domain.Models;

public class ExpenseData
{
    public string? CostCentre { get; set; }
    public decimal? Total { get; set; }
    public string? PaymentMethod { get; set; }

    public decimal? SalesTax { get; set; }
    public decimal? TotalExcludingTax { get; set; }
}
