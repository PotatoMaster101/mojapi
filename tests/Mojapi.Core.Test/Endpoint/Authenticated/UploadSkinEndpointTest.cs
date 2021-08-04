using System;
using System.IO;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Endpoint.Authenticated;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint.Authenticated
{
    /// <summary>
    /// Unit test for <see cref="UploadSkinEndpoint"/>.
    /// </summary>
    public class UploadSkinEndpointTest
    {
        [Theory]
        [InlineData("abc", "def", "classic")]
        public void Constructor_Sets_Members(string access, string url, string variant)
        {
            // arrange, act
            var endpoint = new UploadSkinEndpoint(access, new Skin(url, variant));

            // assert
            Assert.Equal(access, endpoint.Bearer);
            Assert.Equal("multipart/form-data", endpoint.ContentType);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_Throws_OnInvalidBearer(string access)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new UploadSkinEndpoint(access, new Skin("abc")));
        }

        [Fact]
        public void Constructor_Throws_OnNullParams()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentNullException>(() => new UploadSkinEndpoint("abc", null));
        }

        [Theory]
        [InlineData("a")]
        public async Task Request_Throws_OnInvalidBearer(string access)
        {
            // arrange
            await using (File.Create("fakeImage.png")) {}
            var endpoint = new UploadSkinEndpoint(access, new Skin("fakeImage.png"));

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(async () => await endpoint.Request());
        }
    }
}
