using System;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Endpoint.Authentication;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint.Authentication
{
    /// <summary>
    /// Unit test for <see cref="RefreshEndpoint"/>.
    /// </summary>
    public class RefreshEndpointTest
    {
        [Theory]
        [InlineData(@"{""accessToken"":""abc"",""clientToken"":""def""}", "abc", "def")]
        public void Constructor_Sets_Members(string post, string access, string client)
        {
            // arrange, act
            var endpoint = new RefreshEndpoint(new TokenPair(access, client));

            // assert
            Assert.Equal(post, endpoint.PostData);
        }

        [Fact]
        public void Constructor_Throws_OnNullParams()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentNullException>(() => new RefreshEndpoint(null));
        }

        [Theory]
        [InlineData("x", "y")]
        public async Task Request_Throws_OnInvalidTokens(string access, string client)
        {
            // arrange
            var endpoint = new RefreshEndpoint(new TokenPair(access, client));

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseCauseException>(() => endpoint.Request());
        }
    }
}
