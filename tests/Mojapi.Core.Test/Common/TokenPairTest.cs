using System;
using System.Text.Json;
using Mojapi.Core.Common;
using Xunit;

namespace Mojapi.Core.Test.Common
{
    /// <summary>
    /// Unit test for <see cref="TokenPair"/>.
    /// </summary>
    public class TokenPairTest
    {
        [Theory]
        [InlineData("abc", "def", false)]
        [InlineData("abc", null, true)]
        [InlineData("abc", "", true)]
        public void StringConstructor_Sets_Members(string access, string client, bool clientOptional)
        {
            // arrange, act
            var obj = new TokenPair(access, client, clientOptional);

            // assert
            Assert.Equal(access, obj.AccessToken);
            Assert.Equal(client, obj.ClientToken);
            Assert.Equal(clientOptional, obj.ClientOptional);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("good", null)]
        [InlineData(null, "good")]
        [InlineData("", "")]
        [InlineData("good", "")]
        [InlineData("", "good")]
        public void StringConstructor_Throws_OnInvalidParams(string access, string client)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new TokenPair(access, client));
        }

        [Theory]
        [InlineData(@"{""accessToken"":""abc"",""clientToken"":""def""}", "accessToken", "clientToken", "abc", "def", false)]
        [InlineData(@"{""accessToken"":""abc""}", "accessToken", "clientToken", "abc", null, true)]
        public void JsonConstructor_Sets_Members(string json, string accessKey, string clientKey, string access, string client, bool clientOptional)
        {
            // arrange
            using var jsonDoc = JsonDocument.Parse(json);

            // act
            var obj = new TokenPair(jsonDoc.RootElement, accessKey, clientKey);

            // assert
            Assert.Equal(access, obj.AccessToken);
            Assert.Equal(client, obj.ClientToken);
            Assert.Equal(clientOptional, obj.ClientOptional);
        }

        [Theory]
        [InlineData(@"{""accessToken"":""abc""}", "")]
        [InlineData(@"{""accessToken"":""abc""}", "not_found")]
        public void JsonConstructor_Throws_OnInvalidParams(string json, string accessKey)
        {
            // arrange
            using var jsonDoc = JsonDocument.Parse(json);

            // act, assert
            Assert.Throws<ArgumentException>(() => new TokenPair(jsonDoc.RootElement, accessKey));
        }

        [Theory]
        [InlineData(@"""access"":""abc"",""client"":""def""", "access", "client", "abc", "def")]
        public void ToJsonString_Returns_CorrectValue(string json, string accessKey, string clientKey, string access, string client)
        {
            // arrange
            var obj = new TokenPair(access, client);

            // act
            var jsonStr = obj.ToJsonString(accessKey, clientKey);

            // assert
            Assert.Equal(json, jsonStr);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("good", null)]
        [InlineData(null, "good")]
        [InlineData("", "")]
        [InlineData("good", "")]
        [InlineData("", "good")]
        public void ToJsonString_Throws_OnInvalidParams(string accessKey, string clientKey)
        {
            // arrange
            var obj = new TokenPair("abc", "def");

            // act, assert
            Assert.Throws<ArgumentException>(() => obj.ToJsonString(accessKey, clientKey));
        }
    }
}
