using System;
using System.Text.Json;
using Mojapi.Core.Common;
using Xunit;

namespace Mojapi.Core.Test.Common
{
    /// <summary>
    /// Unit test for <see cref="Skin"/>.
    /// </summary>
    public class SkinTest
    {
        [Theory]
        [InlineData("abc", "classic", true, true)]
        [InlineData("abc", "slim", false, true)]
        [InlineData("http://abc", "slim", false, false)]
        [InlineData("https://abc", "slim", false, false)]
        public void StringConstructor_Sets_Members(string url, string variant, bool classic, bool file)
        {
            // arrange, act
            var obj = new Skin(url, variant);

            // assert
            Assert.Equal(url, obj.Url);
            Assert.Equal(variant, obj.Variant);
            Assert.Equal(classic, obj.IsClassic);
            Assert.Equal(file, obj.IsFile);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("good", null)]
        [InlineData(null, "good")]
        [InlineData("", "")]
        [InlineData("good", "")]
        [InlineData("", "good")]
        public void StringConstructor_Throws_OnInvalidParams(string url, string variant)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new Skin(url, variant));
        }

        [Theory]
        [InlineData(@"{""url"":""abc"",""variant"":""classic""}", "url", "variant", "abc", "classic", true)]
        [InlineData(@"{""url"":""abc"",""variant"":""slim""}", "url", "variant", "abc", "slim", false)]
        [InlineData(@"{""url"":""abc""}", "url", "variant", "abc", "classic", true)]
        public void JsonConstructor_Sets_Members(string json, string urlKey, string variantKey, string url, string variant, bool classic)
        {
            // arrange
            using var jsonDoc = JsonDocument.Parse(json);

            // act
            var obj = new Skin(jsonDoc.RootElement, urlKey, variantKey);

            // assert
            Assert.Equal(url, obj.Url);
            Assert.Equal(variant, obj.Variant);
            Assert.Equal(classic, obj.IsClassic);
        }

        [Theory]
        [InlineData(@"{""url"":""abc""}", "")]
        [InlineData(@"{""url"":""abc""}", "not_found")]
        public void JsonConstructor_Throws_OnInvalidParams(string json, string urlKey)
        {
            // arrange
            using var jsonDoc = JsonDocument.Parse(json);

            // act, assert
            Assert.Throws<ArgumentException>(() => new Skin(jsonDoc.RootElement, urlKey));
        }

        [Theory]
        [InlineData(@"""url"":""abc"",""variant"":""slim""", "url", "variant", "abc", "slim")]
        [InlineData(@"""url"":""abc"",""variant"":""classic""", "url", "variant", "abc", "classic")]
        public void ToJsonString_Returns_CorrectValue(string json, string urlKey, string variantKey, string url, string variant)
        {
            // arrange
            var obj = new Skin(url, variant);

            // act
            var jsonStr = obj.ToJsonString(urlKey, variantKey);

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
        public void ToJsonString_Throws_OnInvalidParams(string urlKey, string variantKey)
        {
            // arrange
            var obj = new Skin("abc", "def");

            // act, assert
            Assert.Throws<ArgumentException>(() => obj.ToJsonString(urlKey, variantKey));
        }
    }
}
