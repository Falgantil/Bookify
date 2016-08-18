// Bookify.App
// - Bookify.App.Core.Tests
// -- LoginViewModelTests.cs
// -------------------------------------------
// Author: Bjarke Søgaard <sogaardbjarke@gmail.com>

using System.Net;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.ViewModels;
using Bookify.Common.Models;
using Moq;

using Shouldly;

using Xunit;

namespace Bookify.App.Core.Tests.ViewModels
{
    public class LoginViewModelTests
    {
        /// <summary>
        /// Verifies the login calls authentication on authentication service.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task VerifyLoginCallsAuthenticationOnAuthService()
        {
            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(service => service.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(
                    async (string email, string password) => new PersonDto { Id = 5, Email = email });

            var viewModel = new LoginViewModel(authenticationService.Object)
            {
                Email = "test",
                Password = "test"
            };

            var account = await viewModel.Authenticate();
            account.ShouldNotBeNull();
            account.Id.ShouldBe(5);
            account.Email.ShouldBe("test");
        }

        /// <summary>
        /// Verifies if initial call fails that it retries.
        /// </summary>
        /// <returns></returns>
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
                            return new PersonDto { Id = 5, Email = email };
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
        }

        /// <summary>
        /// Verifies if call fails several times it throws the exception.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task VerifyIfCallFailsSeveralTimesItThrowsTheException()
        {
            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(service => service.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(
                    async (string email, string password) =>
                    {
                        throw new WebException("Should be thrown");
                    });

            var viewModel = new LoginViewModel(authenticationService.Object)
            {
                Email = "test",
                Password = "test"
            };

            await Should.ThrowAsync<WebException>(async () => await viewModel.Authenticate());
        }
    }
}