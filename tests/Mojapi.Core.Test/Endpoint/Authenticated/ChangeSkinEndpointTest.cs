using System;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Endpoint.Authenticated;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint.Authenticated
{
    /// <summary>
    /// Unit test for <see cref="ChangeSkinEndpoint"/>.
    /// </summary>
    public class ChangeSkinEndpointTest
    {
        [Theory]
        [InlineData(@"{""url"":""def"",""variant"":""slim""}", "abc", "def", "slim")]
        [InlineData(@"{""url"":""def"",""variant"":""classic""}", "abc", "def", "classic")]
        public void Constructor_Sets_Members(string post, string access, string skinUrl, string variant)
        {
            // arrange, act
            var endpoint = new ChangeSkinEndpoint(access, new Skin(skinUrl, variant));

            // assert
            Assert.Equal(access, endpoint.Bearer);
            Assert.Equal(post, endpoint.PostData);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("good", null)]
        [InlineData(null, "good")]
        [InlineData("", "")]
        [InlineData("good", "")]
        [InlineData("", "good")]
        public void Constructor_Throws_OnInvalidParams(string access, string skinUrl)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new ChangeSkinEndpoint(access, new Skin(skinUrl)));
        }

        [Theory]
        [InlineData("a")]
        public async Task Request_Throws_OnInvalidBearer(string access)
        {
            // arrange
            var endpoint = new ChangeSkinEndpoint(access, new Skin("good"));

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(async () => await endpoint.Request());
        }
    }
}
