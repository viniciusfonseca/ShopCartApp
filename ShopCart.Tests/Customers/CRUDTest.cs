using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;
using ShopCart.Tests.Utils;

namespace ShopCart.Tests.Customer
{
    public class CRUDTests : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CRUDTests(TestWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        [Fact]
        public async Task CanGetCustomers()
        {
            var _ctx = Utils.Globals.GetContext();
            _ctx.Add(new Models.Customer {
                Name = "Vinicius Fonseca List",
                Gender = "M",
                Email = "vfonseca@example.com"
            });
            await _ctx.SaveChangesAsync();

            var result = new JSONResponse<IEnumerable<Models.Customer>>(
                await _client.GetAsync("/customers"));

            result.Res.EnsureSuccessStatusCode();

            Assert.Contains(await result.Deserialize(), p => p.Name == "Vinicius Fonseca List");
        }

        [Fact]
        public async Task CanCreateCustomer()
        {
            var payload = new {
                name = "Vinicius Fonseca Create",
                email = "vfonseca@example.com",
                gender = "M",
                address = new {
                    street = "Rua XPTO",
                    number = "123",
                    state = "SP",
                    city = "São Paulo"
                }
            };

            var postResult = new JSONResponse<Models.Customer>(
                await _client.PostAsync("/customers", new JSONContent(payload).GetStringContent()));

            postResult.Res.EnsureSuccessStatusCode();
            var createdCustomer = await postResult.Deserialize();
            Assert.Equal(createdCustomer.Name, "Vinicius Fonseca Create");

            var getResult = new JSONResponse<IEnumerable<Models.Customer>>(
                await _client.GetAsync("/customers"));

            getResult.Res.EnsureSuccessStatusCode();

            Assert.Contains(await getResult.Deserialize(), p => p.Name == "Vinicius Fonseca Create");
        }

        [Fact]
        public async Task CanUpdateCustomer()
        {
            
        }
    }
}