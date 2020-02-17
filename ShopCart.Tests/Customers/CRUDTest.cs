using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ShopCart.Tests.Utils;

namespace ShopCart.Tests
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
                Name = "Vinicius Fonseca",
                Gender = "M",
                Email = "vfonseca@example.com"
            });
            await _ctx.SaveChangesAsync();

            var result = new JSONHttpResponseMessage<IEnumerable<Models.Customer>>(
                await _client.GetAsync("/customers")
            );
            result.Result.EnsureSuccessStatusCode();

            Assert.Contains(await result.Deserialize(), p => p.Name == "Vinicius Fonseca");
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

            var postResult = new JSONHttpResponseMessage<Models.Customer>(
                await _client.PostAsync("/customers", new JSONContent(payload).GetStringContent())
            );
            postResult.Result.EnsureSuccessStatusCode();
            var createdCustomer = await postResult.Deserialize();
            Assert.Equal(createdCustomer.Name, "Vinicius Fonseca Create");

            var getResult = new JSONHttpResponseMessage<IEnumerable<Models.Customer>>(
                await _client.GetAsync("/customers")
            );
            getResult.Result.EnsureSuccessStatusCode();

            Assert.Contains(await getResult.Deserialize(), p => p.Name == "Vinicius Fonseca Create");
        }
    }
}