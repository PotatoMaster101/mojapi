using System;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Endpoint.Authentication;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint.Authentication
{
    /// <summary>
    /// Unit test for <see cref="SignOutEndpoint"/>.
    /// </summary>
    public class SignOutEndpointTest
    {
        [Theory]
        [InlineData("abc", "def", @"{""username"":""abc"",""password"":""def""}")]
        [InlineData("  a", "  b", @"{""username"":""  a"",""password"":""  b""}")]
        public void Constructor_Sets_Members(string username, string password, string post)
        {
            // arrange, act
            var endpoint = new SignOutEndpoint(new Credentials(username, password));

            // assert
            Assert.Equal(post, endpoint.PostData);
        }

        [Fact]
        public void Constructor_Throws_OnNullParams()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentNullException>(() => new SignOutEndpoint(null));
        }

        [Theory]
        [InlineData("a", "b")]
        public async Task Request_Throws_OnInvalidBearer(string username, string password)
        {
            // arrange
            var endpoint = new SignOutEndpoint(new Credentials(username, password));

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(() => endpoint.Request());
        }
    }
}
