using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class AuthenticateUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly AuthenticateUserHandler _handler;

        public AuthenticateUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _passwordHasher = Substitute.For<IPasswordHasher>();
            _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
            _handler = new AuthenticateUserHandler(_userRepository, _passwordHasher, _jwtTokenGenerator);
        }

        [Fact]
        public async Task Handle_ValidCredentials_ReturnsSuccessResponse()
        {
            // Given
            var command = new AuthenticateUserCommand
            {
                Email = "test@example.com",
                Password = "password123"
            };

            var user = new User
            {
                Email = command.Email,
                Password = "hashedpassword",
                Username = "TestUser",
                Role = UserRole.Admin,
                Status = UserStatus.Active,
                CreatedAt = DateTime.Now,
                Phone = "8100223366"              
            };

            _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(user);
            _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(true);
            _jwtTokenGenerator.GenerateToken(user).Returns("valid-jwt-token");

            // When
            var act = await _handler.Handle(command, CancellationToken.None);

            // Then
            act.Should().NotBeNull();
            act.Token.Should().Be("valid-jwt-token");
            act.Email.Should().Be(user.Email);
            act.Name.Should().Be(user.Username);
            act.Role.Should().Be(user.Role.ToString());
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_IfUserDoesNotExist()
        {
            // Given
            var command = new AuthenticateUserCommand
            {
                Email = "nonexistent@example.com",
                Password = "password123"
            };

            _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns((User)null!);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("Invalid credentials");
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_IfPasswordIsInvalid()
        {
            // Given
            var command = new AuthenticateUserCommand
            {
                Email = "test@example.com",
                Password = "wrongpassword"
            };

            var user = new User
            {
                Email = command.Email,
                Password = "hashedpassword",
                Username = "TestUser",
                Role = UserRole.Admin,
                Status = UserStatus.Active,
                CreatedAt = DateTime.Now,
                Phone = "8100223366"
            };

            _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(user);
            _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(false);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("Invalid credentials");
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_IfUserIsNotActive()
        {
            // Given
            var command = new AuthenticateUserCommand
            {
                Email = "test@example.com",
                Password = "password123"
            };

            var user = new User
            {
                Email = command.Email,
                Password = "hashedpassword",
                Username = "TestUser",
                Role = UserRole.Admin,
                Status = UserStatus.Inactive,
                CreatedAt = DateTime.Now,
                Phone = "8100223366"
            };

            _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(user);
            _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(true);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("User is not active");
        }
    }
}
