// Bookify.App
// - Bookify.App.Core.Tests
// -- LoginViewModelTests.cs
// -------------------------------------------
// PersonName: Bjarke Søgaard <sogaardbjarke@gmail.com>

using System.Net.Http;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.ViewModels;
using Bookify.App.Sdk.Exceptions;
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
            var authService = new Mock<IAuthenticationService>();
            authService.Setup(service => service.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(
                    async (string email, string password) => new PersonDto { Id = 5, Email = email });

            var viewModel = new LoginViewModel(authService.Object)
            {
                Email = "test",
                Password = "test"
            };

            authService.Verify(s => s.Authenticate("test", "test"), Times.Never);
            await viewModel.Authenticate();
            authService.Verify(s => s.Authenticate("test", "test"), Times.Once);
        }

        /// <summary>
        /// Verifies if initial call fails that it retries.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task VerifyIfInitialCallFailsThatItRetries()
        {
            bool firstCall = true;
            var authService = new Mock<IAuthenticationService>();
            authService.Setup(service => service.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(
                    async (string email, string password) =>
                        {
                            if (firstCall)
                            {
                                firstCall = false;
                                throw await HttpResponseException.CreateException(new HttpResponseMessage());
                            }
                        });

            var viewModel = new LoginViewModel(authService.Object)
            {
                Email = "test",
                Password = "test"
            };

            authService.Verify(s => s.Authenticate("test", "test"), Times.Never);
            await viewModel.Authenticate();
            authService.Verify(s => s.Authenticate("test", "test"), Times.Exactly(2)); // Twice cause the first time it throws, but it's still technically called.
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
                        throw await HttpResponseException.CreateException(new HttpResponseMessage());
                    });

            var viewModel = new LoginViewModel(authenticationService.Object)
            {
                Email = "test",
                Password = "test"
            };

            await Should.ThrowAsync<HttpResponseException>(async () => await viewModel.Authenticate());
        }
    }
}