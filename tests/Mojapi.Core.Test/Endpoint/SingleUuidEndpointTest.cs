using System;
using System.Net;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint
{
    /// <summary>
    /// Unit test for <see cref="SingleUuidEndpoint"/>.
    /// </summary>
    public class SingleUuidEndpointTest
    {
        [Fact]
        public void Constructor_Sets_Members()
        {
            // arrange, act
            var endpoint = new SingleUuidEndpoint("PotatoMaster101");

            // assert
            Assert.Equal("https://api.mojang.com/users/profiles/minecraft/PotatoMaster101", endpoint.Address);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_Throws_OnInvalidUsername(string username)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new SingleUuidEndpoint(username));
        }

        [Fact]
        public async Task Request_Returns_CorrectResponse()
        {
            // arrange
            var endpoint = new SingleUuidEndpoint("PotatoMaster101");

            // act
            var response = await endpoint.Request();

            // assert
            Assert.Equal(HttpStatusCode.OK, response.Status);
            Assert.True(response.Data.Length > 0);
            Assert.Equal("PotatoMaster101", response.Player.Username);
            Assert.Equal("cb2671d590b84dfe9b1c73683d451d1a", response.Player.Uuid);
            Assert.False(response.Player.Legacy);
            Assert.False(response.Player.Demo);
        }

        [Theory]
        [InlineData("ThisUsernameDoesNotExistBecauseItIsTooDamnLong")]
        [InlineData("a")]
        public async Task Request_Throws_OnInvalidUsername(string name)
        {
            // arrange
            var endpoint = new SingleUuidEndpoint(name);

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(async () => await endpoint.Request());
        }
    }
}
