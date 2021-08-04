using System;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint.Authenticated;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint.Authenticated
{
    /// <summary>
    /// Unit test for <see cref="ChangeNameEndpoint"/>.
    /// </summary>
    public class ChangeNameEndpointTest
    {
        [Theory]
        [InlineData("abc", "def")]
        public void Constructor_Sets_Members(string access, string newName)
        {
            // arrange, act
            var endpoint = new ChangeNameEndpoint(access, newName);

            // assert
            Assert.Equal($"https://api.minecraftservices.com/minecraft/profile/name/{newName}", endpoint.Address);
            Assert.Equal(access, endpoint.Bearer);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("good", null)]
        [InlineData(null, "good")]
        [InlineData("", "")]
        [InlineData("good", "")]
        [InlineData("", "good")]
        public void Constructor_Throws_OnInvalidParams(string access, string newName)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new ChangeNameEndpoint(access, newName));
        }

        [Theory]
        [InlineData("a")]
        public async Task Request_Throws_OnInvalidBearer(string access)
        {
            // arrange
            var endpoint = new ChangeNameEndpoint(access, "good");

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(async () => await endpoint.Request());
        }
    }
}
