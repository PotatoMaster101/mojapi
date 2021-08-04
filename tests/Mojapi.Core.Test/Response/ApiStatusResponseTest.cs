using System;
using Mojapi.Core.Response;
using Xunit;

namespace Mojapi.Core.Test.Response
{
    /// <summary>
    /// Unit test for <see cref="ApiStatusResponse"/>.
    /// </summary>
    public class ApiStatusResponseTest
    {
        [Theory]
        [InlineData(ApiStatusResponse.MinecraftService, "green", "Minecraft", ApiStatusResponse.ApiStatus.Green)]
        [InlineData(ApiStatusResponse.SessionService, "red", "Session", ApiStatusResponse.ApiStatus.Red)]
        [InlineData(ApiStatusResponse.AccountService, "yellow", "Account", ApiStatusResponse.ApiStatus.Yellow)]
        [InlineData(ApiStatusResponse.AuthServerService, "unknown", "AuthServer", ApiStatusResponse.ApiStatus.Unknown)]
        [InlineData(ApiStatusResponse.SessionServerService, null, "SessionServer", ApiStatusResponse.ApiStatus.Unknown)]
        [InlineData(ApiStatusResponse.ApiService, " ", "Api", ApiStatusResponse.ApiStatus.Unknown)]
        [InlineData(ApiStatusResponse.TextureService, "red", "Texture", ApiStatusResponse.ApiStatus.Red)]
        [InlineData(ApiStatusResponse.MojangService, "green", "Mojang", ApiStatusResponse.ApiStatus.Green)]
        public void SetStatus_Sets_CorrectStringStatus(string api, string status, string property, ApiStatusResponse.ApiStatus expected)
        {
            // arrange
            var response = new ApiStatusResponse();

            // act
            response.SetStatus(api, status);

            // assert
            Assert.Equal(expected, response.GetType().GetProperty(property)?.GetValue(response));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("fake")]
        public void SetStatus_Throws_OnInvalidApi(string api)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new ApiStatusResponse().SetStatus(api, "green"));
        }

        [Theory]
        [InlineData("green", ApiStatusResponse.ApiStatus.Green)]
        [InlineData("yellow", ApiStatusResponse.ApiStatus.Yellow)]
        [InlineData("red", ApiStatusResponse.ApiStatus.Red)]
        [InlineData("orange", ApiStatusResponse.ApiStatus.Unknown)]
        [InlineData("", ApiStatusResponse.ApiStatus.Unknown)]
        [InlineData(null, ApiStatusResponse.ApiStatus.Unknown)]
        public void ParseStatus_Returns_CorrectStatus(string status, ApiStatusResponse.ApiStatus expected)
        {
            // arrange, act
            var apiStatus = ApiStatusResponse.ParseStatus(status);

            // assert
            Assert.Equal(expected, apiStatus);
        }
    }
}
