using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<Guid, Application.Sales.GetSale.GetSaleCommand>()
            .ConstructUsing(id => new Application.Sales.GetSale.GetSaleCommand(id));
        CreateMap<GetSaleResult, GetSaleResponse>();
        CreateMap<SaleItemResult, SaleItemResponse>();
    }
}