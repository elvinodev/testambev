using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(sale => sale.Customer).NotEmpty().Length(3, 50);
        RuleFor(x => x.SaleNumber).GreaterThan(0);
        RuleFor(x => x.Branch).NotEmpty().MaximumLength(100);
        RuleFor(x => x.SaleDate).LessThanOrEqualTo(DateTime.Now);
        RuleFor(x => x.SaleItem).NotEmpty().Must(x => x.Count > 0);
        RuleForEach(x => x.SaleItem).SetValidator(new SaleItemValidator());
    }
}

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(x => x.Product).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.UnitPrice).GreaterThan(0);
        RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).LessThanOrEqualTo(x => x.UnitPrice * x.Quantity);
    }
}
