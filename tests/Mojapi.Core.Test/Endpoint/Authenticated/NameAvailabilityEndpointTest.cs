using System;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint.Authenticated;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint.Authenticated
{
    /// <summary>
    /// Unit test for <see cref="NameAvailabilityEndpoint"/>.
    /// </summary>
    public class NameAvailabilityEndpointTest
    {
        [Theory]
        [InlineData("abc", "def")]
        public void Constructor_Sets_Members(string access, string name)
        {
            // arrange, act
            var endpoint = new NameAvailabilityEndpoint(access, name);

            // assert
            Assert.Equal($"https://api.minecraftservices.com/minecraft/profile/name/{name}/available", endpoint.Address);
            Assert.Equal(access, endpoint.Bearer);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("good", null)]
        [InlineData(null, "good")]
        [InlineData("", "")]
        [InlineData("good", "")]
        [InlineData("", "good")]
        public void Constructor_Throws_OnInvalidParams(string access, string name)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new NameAvailabilityEndpoint(access, name));
        }

        [Theory]
        [InlineData("a")]
        public async Task Request_Throws_OnInvalidBearer(string access)
        {
            // arrange
            var endpoint = new NameAvailabilityEndpoint(access, "good");

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(async () => await endpoint.Request());
        }
    }
}
