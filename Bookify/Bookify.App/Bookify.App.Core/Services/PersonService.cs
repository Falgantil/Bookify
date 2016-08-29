using System;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    /// <summary>
    /// The Person Service implementation
    /// </summary>
    /// <seealso cref="IPersonService" />
    public class PersonService : IPersonService
    {
        /// <summary>
        /// The API
        /// </summary>
        private readonly IPersonApi api;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        /// <param name="api">The API.</param>
        public PersonService(IPersonApi api)
        {
            this.api = api;
        }

        /// <summary>
        /// Occurs when the subscription state changed.
        /// </summary>
        public event EventHandler<bool> SubscriptionChanged;

        /// <summary>
        /// Gets the Person DTO associated with the currently-authenticated user.
        /// </summary>
        /// <returns></returns>
        public async Task<PersonDto> GetMyself()
        {
            return await this.api.GetMyself();
        }

        /// <summary>
        /// Purchases a subscription on the currently-authenticated user.
        /// </summary>
        /// <returns></returns>
        public async Task PurchaseSubscription()
        {
            await this.api.Subscribe();
            this.SubscriptionChanged?.Invoke(this, true);
        }
    }
}