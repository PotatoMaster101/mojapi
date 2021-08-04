using System;
using System.Text.Json;
using Mojapi.Core.Common;
using Xunit;

namespace Mojapi.Core.Test.Common
{
    /// <summary>
    /// Unit test for <see cref="Property"/>.
    /// </summary>
    public class PropertyTest
    {
        [Theory]
        [InlineData(@"{""name"":""abc"",""value"":""def"",""signature"":""ghi""}", "name", "value", "signature", "abc", "def", "ghi")]
        [InlineData(@"{""name"":""abc"",""value"":""def""}", "name", "value", "signature", "abc", "def", null)]
        public void Constructor_Sets_Members(string json, string nameKey, string valKey, string sigKey, string name, string value, string sig)
        {
            // arrange
            using var jsonDoc = JsonDocument.Parse(json);

            // act
            var obj = new Property(jsonDoc.RootElement, nameKey, valKey, sigKey);

            // assert
            Assert.Equal(name, obj.Name);
            Assert.Equal(value, obj.Value);
            Assert.Equal(sig, obj.Signature);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("a", "b")]
        public void Constructor_Throws_OnInvalidParams(string nameKey, string valKey)
        {
            // arrange
            using var jsonDoc = JsonDocument.Parse(@"{""name"":""abc"",""value"":""def""}");

            // act, assert
            Assert.Throws<ArgumentException>(() => new Property(jsonDoc.RootElement, nameKey, valKey));
        }
    }
}
