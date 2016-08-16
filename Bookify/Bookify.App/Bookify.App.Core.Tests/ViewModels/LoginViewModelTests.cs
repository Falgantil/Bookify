// Bookify.App
// - Bookify.App.Core.Tests
// -- LoginViewModelTests.cs
// -------------------------------------------
// Author: Bjarke Søgaard <sogaardbjarke@gmail.com>

using System.Net;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.ViewModels;
using Bookify.App.Sdk.Exceptions;
using Bookify.Models;

using Moq;

using Shouldly;

using Xunit;

namespace Bookify.App.Core.Tests.ViewModels
{
    public class LoginViewModelTests
    {
        [Fact]
        public async Task VerifyLoginCallsAuthenticationOnAuthService()
        {
            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(service => service.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(
                    async (string email, string password) => new Person { Id = 5, Email = email, Password = password });

            var viewModel = new LoginViewModel(authenticationService.Object)
            {
                Email = "test",
                Password = "test"
            };

            var account = await viewModel.Authenticate();
            account.ShouldNotBeNull();
            account.Id.ShouldBe(5);
            account.Email.ShouldBe("test");
            account.Password.ShouldBe("test");
        }

        [Fact]
        public async Task VerifyIfInitialCallFailsThatItRetries()
        {
            bool firstCall = true;
            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(service => service.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(
                    async (string email, string password) =>
                        {
                            if (firstCall)
                            {
                                firstCall = false;
                                throw new WebException("First call");
                            }
                            return new Person { Id = 5, Email = email, Password = password };
                        });

            var viewModel = new LoginViewModel(authenticationService.Object)
            {
                Email = "test",
                Password = "test"
            };

            var account = await viewModel.Authenticate();
            account.ShouldNotBeNull();
            account.Id.ShouldBe(5);
            account.Email.ShouldBe("test");
            account.Password.ShouldBe("test");
        }
    }
}