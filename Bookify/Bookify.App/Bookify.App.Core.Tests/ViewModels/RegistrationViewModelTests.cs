// Bookify.App
// - Bookify.App.Core.Tests
// -- RegistrationViewModelTests.cs
// -------------------------------------------
// Author: Bjarke Søgaard <sogaardbjarke@gmail.com>

using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.ViewModels;

using Moq;

using Xunit;

namespace Bookify.App.Core.Tests.ViewModels
{
    public class RegistrationViewModelTests
    {
        [Fact]
        public async Task VerifyServiceGetsCalledWithCorrectParameters()
        {
            var authService = new Mock<IAuthenticationService>();
            var viewModel = new RegistrationViewModel(authService.Object)
            {
                FirstName = "1",
                LastName = "2",
                Email = "3",
                Username = "4",
                Password = "5",
                PasswordRepeat = "5"
            };

            await viewModel.Register();

            authService.Verify(s => s.Register("1", "2", "3", "5", "4"), Times.Once);
        }
    }
}