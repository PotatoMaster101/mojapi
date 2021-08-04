using System;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Endpoint.Authentication;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint.Authentication
{
    /// <summary>
    /// Unit test for <see cref="ValidateEndpoint"/>.
    /// </summary>
    public class ValidateEndpointTest
    {
        [Theory]
        [InlineData("abc", null, true, @"{""accessToken"":""abc""}")]
        [InlineData("abc", "", true, @"{""accessToken"":""abc""}")]
        [InlineData("abc", "def", false, @"{""accessToken"":""abc"",""clientToken"":""def""}")]
        public void Constructor_Sets_Members(string access, string client, bool clientOptiona, string post)
        {
            // arrange, act
            var endpoint = new ValidateEndpoint(new TokenPair(access, client, clientOptiona));

            // assert
            Assert.Equal(post, endpoint.PostData);
        }

        [Fact]
        public void Constructor_Throws_OnNullParams()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentNullException>(() => new ValidateEndpoint(null));
        }

        [Theory]
        [InlineData("x")]
        public async Task Request_Throws_OnInvalidBearer(string access)
        {
            // arrange
            var endpoint = new ValidateEndpoint(new TokenPair(access, null, true));

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(() => endpoint.Request());
        }
    }
}
