using System;
using Mojapi.Core.Common;
using Xunit;

namespace Mojapi.Core.Test.Common
{
    /// <summary>
    /// Unit test for <see cref="Credentials"/>.
    /// </summary>
    public class CredentialsTest
    {
        [Theory]
        [InlineData("abc", "def")]
        public void Constructor_Sets_Members(string username, string password)
        {
            // arrange, act
            var obj = new Credentials(username, password);

            // assert
            Assert.Equal(username, obj.Username);
            Assert.Equal(password, obj.Password);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("good", null)]
        [InlineData(null, "good")]
        [InlineData("", "")]
        [InlineData("good", "")]
        [InlineData("", "good")]
        public void Constructor_Throws_OnInvalidParams(string username, string password)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new Credentials(username, password));
        }

        [Theory]
        [InlineData(@"""username"":""abc"",""password"":""def""", "username", "password", "abc", "def")]
        public void ToJsonString_Returns_CorrectJson(string json, string userKey, string passKey, string username, string password)
        {
            // arrange
            var obj = new Credentials(username, password);

            // act
            var jsonStr = obj.ToJsonString(userKey, passKey);

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
        public void ToJsonString_Throws_OnInvalidParams(string userKey, string passKey)
        {
            // arrange
            var obj = new Credentials("abc", "def");

            // act, assert
            Assert.Throws<ArgumentException>(() => obj.ToJsonString(userKey, passKey));
        }
    }
}
