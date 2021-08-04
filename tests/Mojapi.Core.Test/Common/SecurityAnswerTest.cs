using System;
using Mojapi.Core.Common;
using Xunit;

namespace Mojapi.Core.Test.Common
{
    /// <summary>
    /// Unit test for <see cref="SecurityAnswer"/>.
    /// </summary>
    public class SecurityAnswerTest
    {
        [Theory]
        [InlineData(5, "abc")]
        public void constructor_Sets_Members(long id, string answer)
        {
            // arrange, act
            var obj = new SecurityAnswer(id, answer);

            // assert
            Assert.Equal(id, obj.Id);
            Assert.Equal(answer, obj.Answer);
        }

        [Theory]
        [InlineData(5, null)]
        [InlineData(5, "")]
        public void Constructor_Throws_OnInvalidAnswer(long id, string answer)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new SecurityAnswer(id, answer));
        }

        [Theory]
        [InlineData(5, "abc", "id", "answer", @"""id"":5,""answer"":""abc""")]
        [InlineData(10, "def", "xx", "yy", @"""xx"":10,""yy"":""def""")]
        public void ToJsonString_Returns_CorrectValue(long id, string answer, string idKey, string answerKey, string expected)
        {
            // arrange
            var obj = new SecurityAnswer(id, answer);

            // act
            var json = obj.ToJsonString(idKey, answerKey);

            // assert
            Assert.Equal(expected, json);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("good", null)]
        [InlineData(null, "good")]
        [InlineData("", "")]
        [InlineData("good", "")]
        [InlineData("", "good")]
        public void ToJsonString_Throws_OnInvalidParams(string idKey, string answerKey)
        {
            // arrange
            var obj = new SecurityAnswer(5, "abc");

            // act, assert
            Assert.Throws<ArgumentException>(() => obj.ToJsonString(idKey, answerKey));
        }
    }
}
