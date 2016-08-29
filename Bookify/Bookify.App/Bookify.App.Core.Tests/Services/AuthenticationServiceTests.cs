using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Services;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;
using Moq;
using Shouldly;
using Xunit;

namespace Bookify.App.Core.Tests.Services
{
    public class AuthenticationServiceTests
    {
        [Fact]
        public async Task VerifyAuthenticateAndDeauthenticateCallsApis()
        {
            var myself = new PersonDto
            {
                Id = 1,
                Email = "email",
                FirstName = "firstname",
                LastName = "lastname"
            };

            var authApi = new Mock<IAuthenticationApi>();
            var personService = new Mock<IPersonService>();
            authApi.Setup(api => api.Authenticate(It.IsAny<AuthenticateCommand>()))
                .ReturnsAsync(new AuthTokenDto {Person = myself});
            var authService = new AuthenticationService(authApi.Object, personService.Object);
            
            await authService.Authenticate("email", "password");
            authApi.Verify(api => api.Authenticate(It.IsAny<AuthenticateCommand>()), Times.Once);
            authService.LoggedOnAccount.ShouldNotBeNull();
            authService.LoggedOnAccount.Person.ShouldBe(myself);
            authService.LoggedOnAccount.ShouldNotBeNull();

            await authService.Deauthenticate();
            authApi.Verify(api => api.Deauthenticate(), Times.Once);
            authService.LoggedOnAccount.ShouldBeNull();
        }

        [Fact]
        public async Task VerifyAuthenticateAndDeauthenticateInvokesEvents()
        {
            var myself = new PersonDto
            {
                Id = 1,
                Email = "email",
                FirstName = "firstname",
                LastName = "lastname"
            };

            var authApi = new Mock<IAuthenticationApi>();
            var personService = new Mock<IPersonService>();
            authApi.Setup(api => api.Authenticate(It.IsAny<AuthenticateCommand>()))
                .ReturnsAsync(new AuthTokenDto { Person = myself });
            var authService = new AuthenticationService(authApi.Object, personService.Object);
            int changed = 0;
            authService.AuthChanged += (sender, model) => changed++;

            await authService.Authenticate("email", "password");
            changed.ShouldBe(1);
            authService.LoggedOnAccount.ShouldNotBeNull();

            changed = 0;

            await authService.Deauthenticate();
            changed.ShouldBe(1);
            authService.LoggedOnAccount.ShouldBeNull();
        }
    }
}