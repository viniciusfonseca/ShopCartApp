using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShopCart.Tests.Utils
{
    class JSONResponse<T>
    {
        public readonly HttpResponseMessage Res;

        public JSONResponse(HttpResponseMessage res)
        {
            Res = res;
        }

        public async Task<T> Deserialize()
        {
            return JsonConvert.DeserializeObject<T>(await Res.Content.ReadAsStringAsync());
        }
    }
}