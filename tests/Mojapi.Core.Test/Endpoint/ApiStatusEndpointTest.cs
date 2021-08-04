using System.Net;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint;
using Mojapi.Core.Response;
using Xunit;

namespace Mojapi.Core.Test.Endpoint
{
    /// <summary>
    /// Unit test for <see cref="ApiStatusEndpoint"/>.
    /// </summary>
    public class ApiStatusEndpointTest
    {
        [Fact]
        public void Constructor_Sets_CorrectAddress()
        {
            // arrange, act
            var endpoint = new ApiStatusEndpoint();

            // assert
            Assert.Equal("https://status.mojang.com/check", endpoint.Address);
        }

        [Fact]
        public async Task Request_Returns_CorrectResponse()
        {
            // arrange
            var endpoint = new ApiStatusEndpoint();

            // act
            var response = await endpoint.Request();

            // assert
            Assert.Equal(HttpStatusCode.OK, response.Status);
            Assert.True(response.Data.Length > 0);
            Assert.True(response.Minecraft != ApiStatusResponse.ApiStatus.Unknown);
            Assert.True(response.Session != ApiStatusResponse.ApiStatus.Unknown);
            Assert.True(response.Account != ApiStatusResponse.ApiStatus.Unknown);
            Assert.True(response.AuthServer != ApiStatusResponse.ApiStatus.Unknown);
            Assert.True(response.SessionServer != ApiStatusResponse.ApiStatus.Unknown);
            Assert.True(response.Api != ApiStatusResponse.ApiStatus.Unknown);
            Assert.True(response.Texture != ApiStatusResponse.ApiStatus.Unknown);
            Assert.True(response.Mojang != ApiStatusResponse.ApiStatus.Unknown);
        }
    }
}
