using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class DeleteSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly DeleteSaleHandler _handler;

        public DeleteSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _mediator = Substitute.For<IMediator>();
            _handler = new DeleteSaleHandler(_saleRepository, _mediator);
        }

        [Fact(DisplayName = "Given valid Sale data When getting Sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = DeleteSaleHandlerTestData.GenerateValidCommand();
            var sale = SaleTestData.GenerateValidSale();
            var result = GetSaleResultTestData.GenerateValidResult();

            _saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(true);

            // Act
            var act = await _handler.Handle(command, CancellationToken.None);

            // Then
            act.Should().NotBeNull();
            act.Success.Should().BeTrue();

            await _mediator.Received(1).Publish(Arg.Is<SaleDeletedEvent>(e =>
                   e.SaleId == command.Id
               ), CancellationToken.None);
        }

        [Fact(DisplayName = "Given invalid sale data When getting sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new DeleteSaleCommand(Guid.Empty); 

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact(DisplayName = "Given invalid sale data When getting sale Then throws data exception")]
        public async Task Handle_InvalidRequest_ThrowsDataException()
        {
            // Given
            var command = DeleteSaleHandlerTestData.GenerateValidCommand();
            Sale sale = null!;

            _saleRepository.GetByIdItemAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
