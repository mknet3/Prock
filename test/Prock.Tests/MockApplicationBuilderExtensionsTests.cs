using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Prock.Core;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Prock.Tests
{
    public class MockApplicationBuilderExtensionsTests
    {
        [Fact]
        public async Task Mock_WhenARequestIsSentWithMockRoute_ShouldReturnsMockParameters()
        {
            var mock = new MockTest
            {
                Route = "/test",
                ContentType = "plain/text",
                StatusCode = 400,
                Json = @"{""hello"":""world""}"
            };
            using (var server = new TestServer(new WebHostBuilder().Configure(app => app.UseMock(mock))))
            {
                var response = await server.CreateRequest(mock.Route).GetAsync();
                Assert.Equal(mock.StatusCode, (int)response.StatusCode);

                var contentType = response.Content.Headers.GetValues("Content-Type").FirstOrDefault();
                Assert.Equal(mock.ContentType, contentType);

                var body = await response.Content.ReadAsStringAsync();
                Assert.Equal(mock.Json, body);
            }
        }

        public class MockTest : IMock
        {
            public string ContentType { get; set; }

            public string Json { get; set; }

            public string Route { get; set; }

            public int StatusCode { get; set; }
        }
    }
}
