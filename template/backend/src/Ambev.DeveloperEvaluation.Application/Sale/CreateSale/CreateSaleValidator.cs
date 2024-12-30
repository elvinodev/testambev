using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(sale => sale.Customer).NotEmpty().Length(3, 50);
        RuleFor(x => x.SaleNumber).GreaterThan(0);
        RuleFor(x => x.Branch).NotEmpty().MaximumLength(100);
        RuleFor(x => x.SaleDate).LessThanOrEqualTo(DateTime.Now);
        RuleFor(x => x.SaleItem).NotEmpty().Must(x => x.Count > 0);
        RuleForEach(x => x.SaleItem).SetValidator(new CreateSaleItemCommandValidator());
    }
}

public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
{
    public CreateSaleItemCommandValidator()
    {
        RuleFor(x => x.Product).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.UnitPrice).GreaterThan(0);
        RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).LessThanOrEqualTo(x => x.UnitPrice * x.Quantity);
    }
}
