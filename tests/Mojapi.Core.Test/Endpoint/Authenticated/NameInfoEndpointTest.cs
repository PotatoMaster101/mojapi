using System;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint.Authenticated;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint.Authenticated
{
    /// <summary>
    /// Unit test for <see cref="NameInfoEndpoint"/>.
    /// </summary>
    public class NameInfoEndpointTest
    {
        [Theory]
        [InlineData("abc")]
        public void Constructor_Sets_Members(string access)
        {
            // arrange, act
            var endpoint = new NameInfoEndpoint(access);

            // assert
            Assert.Equal(access, endpoint.Bearer);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_Throws_OnInvalidParams(string access)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new NameInfoEndpoint(access));
        }

        [Theory]
        [InlineData("a")]
        public async Task Request_Throws_OnInvalidBearer(string access)
        {
            // arrange
            var endpoint = new NameInfoEndpoint(access);

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(async () => await endpoint.Request());
        }
    }
}
