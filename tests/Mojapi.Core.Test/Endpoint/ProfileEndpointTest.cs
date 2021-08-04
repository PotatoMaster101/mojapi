using System;
using System.Net;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint
{
    /// <summary>
    /// Unit test for <see cref="ProfileEndpoint"/>.
    /// </summary>
    public class ProfileEndpointTest
    {
        [Fact]
        public void Constructor_Sets_Members()
        {
            // arrange
            const string uuid = "12345678123412341234123456789012";

            // act
            var endpoint = new ProfileEndpoint(uuid);

            // assert
            Assert.Equal($"https://sessionserver.mojang.com/session/minecraft/profile/{uuid}", endpoint.Address);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_Throws_OnInvalidUuid(string uuid)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new ProfileEndpoint(uuid));
        }

        [Theory]
        [InlineData("PotatoMaster101", "cb2671d590b84dfe9b1c73683d451d1a", "http://textures.minecraft.net/texture/8162f5b97d015a9d6b1c5e783af48701bc82b55a4f59b5e6e05749d06b8ef8", null, false)]
        [InlineData("Dinnerbone", "61699b2ed3274a019f1e0ea8c3f06bc6", "http://textures.minecraft.net/texture/50c410fad8d9d8825ad56b0e443e2777a6b46bfa20dacd1d2f55edc71fbeb06d", "http://textures.minecraft.net/texture/5786fe99be377dfb6858859f926c4dbc995751e91cee373468c5fbf4865e7151", false)]
        [InlineData("_QueenKathleen_", "79217508a0484a78abba52c66049f028", "http://textures.minecraft.net/texture/2a5f8e35116ad0ad6cff9ff1eaeeaf0c67acc9b2a66dbf1f87a18cba34abf5dd", null, true)]
        [InlineData("PotatoMaster101", "cb2671d590b84dfe9b1c73683d451d1a", "http://textures.minecraft.net/texture/8162f5b97d015a9d6b1c5e783af48701bc82b55a4f59b5e6e05749d06b8ef8", null, false, false)]
        public async Task Request_Returns_CorrectResponse(string username, string uuid, string skinUrl, string capeUrl, bool slim, bool unsigned = true)
        {
            // arrange
            var endpoint = new ProfileEndpoint(uuid, unsigned);

            // act
            var response = await endpoint.Request();

            // assert
            Assert.Equal(HttpStatusCode.OK, response.Status);
            Assert.True(response.Data.Length > 0);
            Assert.Equal(unsigned, endpoint.Unsigned);
            Assert.Equal(username, response.Player.Username);
            Assert.Equal(uuid, response.Player.Uuid);
            Assert.Equal(skinUrl, response.Texture.SkinUrl);
            Assert.Equal(capeUrl, response.Texture.CapeUrl);
            Assert.Equal(slim, response.Texture.SlimSkin);
            Assert.False(response.Player.Legacy);
        }

        [Theory]
        [InlineData("12345678123412341234123456789012")]
        [InlineData("000")]
        public async Task Request_Throws_OnBadUuid(string uuid)
        {
            // arrange
            var endpoint = new ProfileEndpoint(uuid);

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(async () => await endpoint.Request());
        }
    }
}
