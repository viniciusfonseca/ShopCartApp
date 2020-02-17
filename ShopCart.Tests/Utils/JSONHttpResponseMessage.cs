using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShopCart.Tests.Utils
{
    class JSONHttpResponseMessage<T>
    {
        public readonly HttpResponseMessage Result;

        public JSONHttpResponseMessage(HttpResponseMessage result)
        {
            Result = result;
        }
        public async Task<T> Deserialize()
        {
            return JsonConvert.DeserializeObject<T>(await Result.Content.ReadAsStringAsync());
        }
    }
}