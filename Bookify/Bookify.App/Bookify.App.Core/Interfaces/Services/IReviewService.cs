using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.App.Core.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewModel>> GetReviews(int bookId);
    }
}