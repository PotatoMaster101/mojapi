using System;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint.Authenticated;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint.Authenticated
{
    /// <summary>
    /// Unit test for <see cref="ResetSkinEndpoint"/>.
    /// </summary>
    public class ResetSkinEndpointTest
    {
        [Theory]
        [InlineData("abc", "def")]
        public void Constructor_Sets_Members(string access, string uuid)
        {
            // arrange, act
            var endpoint = new ResetSkinEndpoint(access, uuid);

            // assert
            Assert.Equal(access, endpoint.Bearer);
            Assert.Equal($"https://api.mojang.com/user/profile/{uuid}/skin", endpoint.Address);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("good", null)]
        [InlineData(null, "good")]
        [InlineData("", "")]
        [InlineData("good", "")]
        [InlineData("", "good")]
        public void Constructor_Throws_OnInvalidParams(string access, string uuid)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new ResetSkinEndpoint(access, uuid));
        }

        [Theory]
        [InlineData("a")]
        public async Task Request_Returns_CorrectValue(string access)
        {
            // arrange
            var endpoint = new ResetSkinEndpoint(access, "good");

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(() => endpoint.Request());
        }
    }
}
