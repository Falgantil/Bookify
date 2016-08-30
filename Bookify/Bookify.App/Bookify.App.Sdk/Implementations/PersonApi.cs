using System.Net.Http;
using System.Threading.Tasks;

using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Models;

using Newtonsoft.Json;

namespace Bookify.App.Sdk.Implementations
{
    public class PersonApi : BaseApi, IPersonApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonApi"/> class.
        /// </summary>
        public PersonApi()
            : base(ApiConfig.PersonRoot)
        {
        }

        /// <summary>
        /// Gets my own Person object.
        /// </summary>
        /// <returns></returns>
        public async Task<PersonDto> GetMyself()
        {
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("me"));

            var response = await this.ExecuteRequest(request);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PersonDto>(json);
        }

        /// <summary>
        /// Creates a subscription.
        /// </summary>
        /// <returns></returns>
        public async Task Subscribe()
        {
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("subscribe"))
                .Method(HttpMethod.Post);

            await this.ExecuteRequest(request);
        }
    }
}