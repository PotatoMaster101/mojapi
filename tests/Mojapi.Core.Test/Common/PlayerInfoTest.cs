using System.Text.Json;
using Mojapi.Core.Common;
using Xunit;

namespace Mojapi.Core.Test.Common
{
    /// <summary>
    /// Unit test for <see cref="PlayerInfo"/>.
    /// </summary>
    public class PlayerInfoTest
    {
        [Theory]
        [InlineData(@"{""name"":""abc"",""id"":""def"",""legacy"":true,""demo"":true}", "abc", "def", true, true)]
        [InlineData(@"{""name"":""a"",""id"":""b"",""legacy"":true}", "a", "b", true, false)]
        [InlineData(@"{""name"":""x"",""id"":""y"",""demo"":true}", "x", "y", false, true)]
        public void Constructor_Sets_Members(string json, string username, string uuid, bool legacy, bool demo)
        {
            // arrange
            using var jsonDoc = JsonDocument.Parse(json);

            // act
            var obj = new PlayerInfo(jsonDoc.RootElement);

            // assert
            Assert.Equal(username, obj.Username);
            Assert.Equal(uuid, obj.Uuid);
            Assert.Equal(legacy, obj.Legacy);
            Assert.Equal(demo, obj.Demo);
        }
    }
}
