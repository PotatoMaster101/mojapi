using System;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Endpoint.Authentication;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint.Authentication
{
    /// <summary>
    /// Unit test for <see cref="InvalidateEndpoint"/>.
    /// </summary>
    public class InvalidateEndpointTest
    {
        [Theory]
        [InlineData(@"{""accessToken"":""abc"",""clientToken"":""def""}", "abc", "def")]
        [InlineData(@"{""accessToken"":""def"",""clientToken"":""abc""}", "def", "abc")]
        public void Constructor_Sets_Members(string post, string access, string client)
        {
            // arrange, act
            var endpoint = new InvalidateEndpoint(new TokenPair(access, client));

            // assert
            Assert.Equal(post, endpoint.PostData);
        }

        [Fact]
        public void Constructor_Throws_OnNullParams()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentNullException>(() => new InvalidateEndpoint(null));
        }

        [Theory]
        [InlineData("a", "b")]
        public async Task Request_Throws_OnInvalidBearer(string access, string client)
        {
            // arrange
            var endpoint = new InvalidateEndpoint(new TokenPair(access, client));

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(() => endpoint.Request());
        }
    }
}
