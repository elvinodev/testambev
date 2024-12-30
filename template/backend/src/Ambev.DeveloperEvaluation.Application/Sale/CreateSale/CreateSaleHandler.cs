using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _mediator = mediator;
    }


    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);

        if (existingSale != null)
            throw new InvalidOperationException($"Sale with id {command.Id} already exists");

        var sale = _mapper.Map<Sale>(command);

        sale.SaleItem.ForEach(e => e.ApplyDiscount());
        sale.CalculateTotalAmount();

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        var saleEvent = new SaleRegisteredEvent(sale);
        await _mediator.Publish(saleEvent, cancellationToken);

        return _mapper.Map<CreateSaleResult>(createdSale);
    }
}
