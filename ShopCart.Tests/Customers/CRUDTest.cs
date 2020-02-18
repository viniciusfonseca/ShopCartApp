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
            var _ctx = Globals.GetContext();
            _ctx.Customers.Add(new Models.Customer {
                Name = $"Vinicius Fonseca List - {ID.Generate()}",
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
                name = $"Vinicius Fonseca Create - {ID.Generate()}",
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
            Assert.Equal(createdCustomer.Name, payload.name);

            var getResult = new JSONResponse<IEnumerable<Models.Customer>>(
                await _client.GetAsync("/customers"));

            getResult.Res.EnsureSuccessStatusCode();

            Assert.Contains(await getResult.Deserialize(), p => p.Name == payload.name);
        }

        [Fact]
        public async Task CanUpdateCustomer()
        {
            var _ctx = Globals.GetContext();
            var customer = _ctx.Customers.Add(new Models.Customer {
                Name = $"Vinicius Fonseca Updated - {ID.Generate()}",
                Gender = "M",
                Email = "vfonseca@example.com"
            }).Entity;
            await _ctx.SaveChangesAsync();

            var payload = new {
                name = $"Vinicius Fonseca Updated 2 - {ID.Generate()}",
                email = "vfonseca@example.com",
                gender = "M",
                address = new {
                    street = "Rua XPTO",
                    number = "123",
                    state = "SP",
                    city = "São Paulo"
                }
            };

            var putResult = new JSONResponse<Models.Customer>(
                await _client.PutAsync($"/customers/{customer.Id}", new JSONContent(payload).GetStringContent())
            );

            putResult.Res.EnsureSuccessStatusCode();
            var updatedCustomer = await putResult.Deserialize();
            Assert.Equal(updatedCustomer.Name, payload.name);

            var getResult = new JSONResponse<Models.Customer>(
                await _client.GetAsync($"/customers/{customer.Id}")
            );

            getResult.Res.EnsureSuccessStatusCode();
            var customerFromGet = await getResult.Deserialize();
            Assert.Equal(customerFromGet.Name, payload.name);
        }

        [Fact]
        public async Task CanDeleteCustomer()
        {
            var _ctx = Globals.GetContext();
            var customer = _ctx.Customers.Add(new Models.Customer {
                Name = $"Vinicius Fonseca Delete - {ID.Generate()}",
                Gender = "M",
                Email = "vfonseca@example.com"
            }).Entity;
            await _ctx.SaveChangesAsync();

            var deleteResult = await _client.DeleteAsync($"/customers/{customer.Id}");
            deleteResult.EnsureSuccessStatusCode();

            var getResult = await _client.GetAsync($"/customers/{customer.Id}");
            Assert.Equal(getResult.StatusCode, System.Net.HttpStatusCode.NotFound);
        }
    }
}