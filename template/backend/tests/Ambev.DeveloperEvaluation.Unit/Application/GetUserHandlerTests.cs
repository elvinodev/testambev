using Ambev.DeveloperEvaluation.Application.Users.GetUser;
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
    public class GetUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly GetUserHandler _handler;

        public GetUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _mediator = Substitute.For<IMediator>();
            _handler = new GetUserHandler(_userRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid User data When getting User Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = GetUserHandlerTestData.GenerateValidCommand();
            var user = UserTestData.GenerateValidUser();
            var result = GetUserResultTestData.GenerateValidResult();

            _userRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(user);
            
            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Then
            _mapper.Map<GetUserResult>(user).Returns(result);
        }

        [Fact(DisplayName = "Given invalid User data When getting User Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new GetUserCommand(Guid.Empty); 

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact(DisplayName = "Given invalid User data When getting User Then throws data exception")]
        public async Task Handle_InvalidRequest_ThrowsDataException()
        {
            // Given
            var command = GetUserHandlerTestData.GenerateValidCommand();
            User user = null!;

            _userRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(user);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
