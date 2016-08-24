using System;
using System.Threading.Tasks;
using Bookify.Common.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    /// <summary>
    /// The Person Service interface
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Occurs when the subscription state changed.
        /// </summary>
        event EventHandler<bool> SubscriptionChanged;

        /// <summary>
        /// Gets the Person DTO associated with the currently-authenticated user.
        /// </summary>
        /// <returns></returns>
        Task<PersonDto> GetMyself();

        /// <summary>
        /// Purchases a subscription on the currently-authenticated user.
        /// </summary>
        /// <returns></returns>
        Task PurchaseSubscription();
    }
}