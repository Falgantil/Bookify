using System;
using System.Threading.Tasks;

using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Models;

using Newtonsoft.Json;

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
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("me"));

            var response = await this.ExecuteRequest(request);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PersonDto>(json);
        }
    }
}