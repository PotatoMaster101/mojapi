using System;
using System.Text.Json;
using Mojapi.Core.Common;
using Xunit;

namespace Mojapi.Core.Test.Common
{
    /// <summary>
    /// Unit test for <see cref="Player"/>.
    /// </summary>
    public class PlayerTest
    {
        [Theory]
        [InlineData(@"{""name"":""abc"",""id"":""def""}", "name", "id", "abc", "def")]
        [InlineData(@"{""abc"":""name"",""def"":""id""}", "abc", "def", "name", "id")]
        public void Constructor_Sets_Members(string json, string nameKey, string idKey, string name, string id)
        {
            // arrange
            using var doc = JsonDocument.Parse(json);

            // act
            var obj = new Player(doc.RootElement, nameKey, idKey);

            // assert
            Assert.Equal(name, obj.Username);
            Assert.Equal(id, obj.Uuid);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("a", "b")]
        public void Constructor_Throws_OnInvalidParams(string nameKey, string idKey)
        {
            // arrange
            using var doc = JsonDocument.Parse(@"{""name"":""abc"",""id"":""def""}");

            // act, assert
            Assert.Throws<ArgumentException>(() => new Player(doc.RootElement, nameKey, idKey));
        }
    }
}
