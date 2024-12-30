using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
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
    public class DeleteUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly DeleteUserHandler _handler;

        public DeleteUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _mediator = Substitute.For<IMediator>();
            _handler = new DeleteUserHandler(_userRepository);
        }

        [Fact(DisplayName = "Given valid User data When getting User Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = DeleteUserHandlerTestData.GenerateValidCommand();
            var user = UserTestData.GenerateValidUser();
            var result = GetUserResultTestData.GenerateValidResult();

            _userRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(true);

            // Act
            var act = await _handler.Handle(command, CancellationToken.None);

            // Then
            act.Should().NotBeNull();
            act.Success.Should().BeTrue();
        }

        [Fact(DisplayName = "Given invalid sale data When getting sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new DeleteUserCommand(Guid.Empty); 

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact(DisplayName = "Given invalid sale data When getting sale Then throws data exception")]
        public async Task Handle_InvalidRequest_ThrowsDataException()
        {
            // Given
            var command = DeleteUserHandlerTestData.GenerateValidCommand();
            User user = null!;

            _userRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(user);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
