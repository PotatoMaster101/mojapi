using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint;
using Xunit;

namespace Mojapi.Core.Test.Endpoint
{
    /// <summary>
    /// Unit test for <see cref="BlockedServerEndpoint"/>.
    /// </summary>
    public class BlockedServerEndpointTest
    {
        [Fact]
        public async Task Request_Returns_CorrectResponse()
        {
            // arrange
            var endpoint = new BlockedServerEndpoint();

            // act
            var response = await endpoint.Request();

            // assert
            Assert.Equal(HttpStatusCode.OK, response.Status);
            Assert.True(response.Data.Length > 0);
            Assert.True(response.Hashes.Any());
        }
    }
}
