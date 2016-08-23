using System.Collections.Generic;
using System.Threading.Tasks;

using Bookify.App.Core.Models;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface IFeedbackService : IGetByFilterService<FeedbackDto, FeedbackFilter>
    {

    }
}