using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

namespace ShopCart.Tests.Utils
{
    class JSONContent
    {
        dynamic _json;

        public JSONContent(dynamic json)
        {
            _json = JsonConvert.SerializeObject(json);
        }

        public StringContent GetStringContent()
        {
            return new StringContent(_json, Encoding.UTF8, "application/json");
        }
    }
}