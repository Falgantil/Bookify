using System;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Services;
using Bookify.App.Core.ViewModels;
using Bookify.Common.Models;
using Moq;
using Shouldly;
using Xunit;

namespace Bookify.App.Core.Tests.ViewModels
{
    public class CreateReviewViewModelTests
    {
        [Fact]
        public void VerifyViewModelCannotBeCreatedWhenNotLoggedIn()
        {
            var book = new DetailedBookDto();
            var authenticationService = new Mock<IAuthenticationService>();
            var feedbackService = new Mock<IFeedbackService>();

            Should.Throw<ArgumentNullException>(
                () => new CreateReviewViewModel(book, authenticationService.Object, feedbackService.Object));
        }

        [Fact]
        public void VerifyViewModelCanBeCreatedWhenLoggedIn()
        {
            var book = new DetailedBookDto();
            var authenticationService = new Mock<IAuthenticationService>();
            var authToken = new AuthTokenDto
            {
                Person = new PersonDto()
            };
            AccountModel loggedOnUser = new AccountModel(authToken);
            authenticationService.SetupGet(s => s.LoggedOnAccount).Returns(() => loggedOnUser);
            var feedbackService = new Mock<IFeedbackService>();

            var viewModel = new CreateReviewViewModel(book, authenticationService.Object, feedbackService.Object);
            viewModel.ShouldNotBeNull();
        }

        [Fact]
        public async Task VerifyCreatingReviewCallsApiWhenDataIsFilled()
        {
            var book = new DetailedBookDto();
            var authService = new Mock<IAuthenticationService>();
            AccountModel loggedOnUser = new AccountModel(new AuthTokenDto
            {
                Person = new PersonDto()
            });
            authService.SetupGet(s => s.LoggedOnAccount).Returns(() => loggedOnUser);
            var feedbackService = new Mock<IFeedbackService>();

            var viewModel = new CreateReviewViewModel(book, authService.Object, feedbackService.Object);

            await Should.ThrowAsync<InvalidOperationException>(async () => await viewModel.CreateReview());
            feedbackService.Verify(s => s.CreateFeedback(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Never);

            viewModel.Rating = 3;
            await Should.ThrowAsync<InvalidOperationException>(async () => await viewModel.CreateReview());
            feedbackService.Verify(s => s.CreateFeedback(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Never);

            viewModel.Message = "Message";
            var dto = await viewModel.CreateReview();
            feedbackService.Verify(s => s.CreateFeedback(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task VerifyValidCreateCallsApi()
        {
            var book = new DetailedBookDto {Id = 5};
            var authService = new Mock<IAuthenticationService>();
            AccountModel loggedOnUser = new AccountModel(new AuthTokenDto
            {
                Person = new PersonDto()
            });
            authService.SetupGet(s => s.LoggedOnAccount).Returns(() => loggedOnUser);
            var feedbackService = new Mock<IFeedbackService>();

            var viewModel = new CreateReviewViewModel(book, authService.Object, feedbackService.Object)
            {
                Message = "test",
                Rating = 2
            };

            var feedback = new BookFeedbackDto();
            feedbackService.Setup(s => s.CreateFeedback(5, 2, "test")).ReturnsAsync(feedback);
            var dto = await viewModel.CreateReview();
            dto.ShouldBe(feedback);
            feedbackService.Verify(s => s.CreateFeedback(5, 2, "test"), Times.Once);
        }
    }
}