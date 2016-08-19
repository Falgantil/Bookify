using System;
using System.Threading.Tasks;

using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Models;

namespace Bookify.App.Sdk.Implementations
{
    public class PersonApi : BaseApi, IPersonApi
    {
        public PersonApi()
            : base(ApiConfig.PersonRoot)
        {
        }

        public async Task<PersonDto> GetMyself()
        {
            throw new NotImplementedException();
        }
    }
}