using System;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Endpoint.Authentication;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint.Authentication
{
    /// <summary>
    /// Unit test for <see cref="AuthenticationEndpoint"/>.
    /// </summary>
    public class AuthenticationEndpointTest
    {
        [Theory]
        [InlineData("abc", "def", true)]
        [InlineData("def", "abc", false)]
        public void Constructor_Sets_Members(string username, string password, bool request)
        {
            // arrange, act
            var endpoint = new AuthenticationEndpoint(new Credentials(username, password), request);

            // assert
            Assert.Contains(@"{""agent"":{""name"":""Minecraft"",""version"":1},", endpoint.PostData);
            Assert.Contains($@"""username"":""{username}"",", endpoint.PostData);
            Assert.Contains($@"""password"":""{password}"",", endpoint.PostData);
            Assert.Contains($@"""clientToken"":""", endpoint.PostData);
            Assert.Contains($@"""requestUser"":{request.ToString().ToLower()}}}", endpoint.PostData);
        }

        [Fact]
        public void Constructor_Throws_OnNullParams()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentNullException>(() => new AuthenticationEndpoint(null));
        }

        [Theory]
        [InlineData("a", "b")]
        public async Task Request_Throws_OnInvalidCredentials(string username, string password)
        {
            // arrange
            var endpoint = new AuthenticationEndpoint(new Credentials(username, password));

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseCauseException>(async () => await endpoint.Request());
        }
    }
}
