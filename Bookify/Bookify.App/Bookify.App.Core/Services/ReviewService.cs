using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;

namespace Bookify.App.Core.Services
{
    public class ReviewService : IReviewService
    {
        public async Task<IEnumerable<ReviewModel>> GetReviews(int bookId)
        {
            await Task.Delay(250);

            return new[]
            {
                new ReviewModel
                {
                    Id = 1,
                    CreatedTs = DateTime.Now.AddHours(-5),
                    Author = "Bjarke Søgaard",
                    Rating = 3,
                    Message = "This is a pretty decent book. Just kidding, I haven't actually read it. Sorry :("
                },
                new ReviewModel
                {
                    Id = 2,
                    CreatedTs = DateTime.Now.AddDays(-50),
                    Author = "Jonas Thorsen",
                    Rating = 1,
                    Message = "Wat teh fak is dis, I don't read books! U FGT"
                },
                new ReviewModel
                {
                    Id = 3,
                    CreatedTs = DateTime.Now.AddDays(-50),
                    Author = "Andreas Hansen",
                    Rating = 5,
                    Message = "I really like this book! This is a really good book. Like, wow! So amazing. More!"
                },
                new ReviewModel
                {
                    Id = 4,
                    CreatedTs = DateTime.Now.AddDays(-50),
                    Author = "Rick Boysen",
                    Rating = 2,
                    Message = "Erh... Sure, it's alright. Could be better, could be worse. But could definitely be better."
                }
            };
        }
    }
}