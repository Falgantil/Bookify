using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackApi api;

        public FeedbackService(IFeedbackApi api)
        {
            this.api = api;
        }
        
        public async Task<IPaginatedEnumerable<FeedbackDto>> GetItems(FeedbackFilter filter)
        {
            return await this.api.GetFeedback(filter);
        }
    }
}