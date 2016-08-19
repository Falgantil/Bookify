using System.Threading.Tasks;

using Bookify.Common.Models;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IPersonApi
    {
        Task<PersonDto> GetMyself();
    }
}