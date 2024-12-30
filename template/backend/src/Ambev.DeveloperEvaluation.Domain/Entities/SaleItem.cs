using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class SaleItem : BaseEntity
{
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }

    public void ApplyDiscount()
    {
        if (Quantity >= 4 && Quantity < 10)
            Discount = (UnitPrice * Quantity) * 0.1m;
        else if (Quantity >= 10 && Quantity <= 20)
            Discount = (UnitPrice * Quantity) * 0.2m;
        else if (Quantity > 20)
            throw new InvalidOperationException("Cannot sell more than 20 identical items.");
    }

    public decimal TotalItemAmount()
    {
        return (UnitPrice * Quantity) - Discount;
    }
}

