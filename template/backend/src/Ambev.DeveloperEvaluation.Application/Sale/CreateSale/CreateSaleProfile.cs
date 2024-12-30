using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

[ExcludeFromCodeCoverage]
public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>();
        CreateMap<Sale, CreateSaleResult>();
    }
}