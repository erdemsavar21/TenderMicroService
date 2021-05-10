using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Tender.Order.Test
{
    public class OrderControllerTest : IClassFixture<InMemoryWebApplicationFactory<Startup>>
    {

        private InMemoryWebApplicationFactory<Startup> _factory;

        public OrderControllerTest(InMemoryWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task OrderController_Api_IsSuccesfully()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/v1/Order/GetOrdersByUserName/erdem@test.com");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
