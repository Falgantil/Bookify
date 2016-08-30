using System.Threading.Tasks;

using Bookify.Common.Models;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IPersonApi
    {
        /// <summary>
        /// Gets my own Person object.
        /// </summary>
        /// <returns></returns>
        Task<PersonDto> GetMyself();

        /// <summary>
        /// Creates a subscription.
        /// </summary>
        /// <returns></returns>
        Task Subscribe();
    }
}