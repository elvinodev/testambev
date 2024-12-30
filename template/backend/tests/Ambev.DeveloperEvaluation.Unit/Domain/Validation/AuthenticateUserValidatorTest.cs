using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class AuthenticateUserValidatorTest
    {
        [Theory(DisplayName = "Given a phone number When validating Then should validate according to regex pattern")]
        [InlineData("jcampos@gmail.com", "@234crips76")]               
        public void Given_Email_When_Validating_Then_ShouldValidateAccordingToData(string email, string password)
        {
            // Arrange
            var validator = new AuthenticateUserValidator();

            // Act
            var result = validator.Validate(new AuthenticateUserCommand
            {
                Email = email,
                Password = password
            });

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
