using System;
using System.Text.Json;
using Mojapi.Core.Common;
using Xunit;

namespace Mojapi.Core.Test.Common
{
    /// <summary>
    /// Unit test for <see cref="SecurityQuestion"/>.
    /// </summary>
    public class SecurityQuestionTest
    {
        [Theory]
        [InlineData(@"{""id"":3,""question"":""abc""}", "id", "question", 3, "abc")]
        [InlineData(@"{""id"":-1,""question"":""abc""}", "id", "question", -1, "abc")]
        public void JsonConstructor_Sets_Members(string json, string idKey, string questionKey, long id, string question)
        {
            // arrange
            using var jsonDoc = JsonDocument.Parse(json);

            // act
            var obj = new SecurityQuestion(jsonDoc.RootElement, idKey, questionKey);

            // assert
            Assert.Equal(id, obj.Id);
            Assert.Equal(question, obj.Question);
        }

        [Theory]
        [InlineData(@"{""id"":3,""question"":""abc""}", "not_found", "not_found")]
        [InlineData(@"{""id"":3,""question"":""abc""}", "id", "not_found")]
        [InlineData(@"{""id"":3,""question"":""abc""}", "not_found", "question")]
        public void JsonConstructor_Throws_OnInvalidParams(string json, string idKey, string questionKey)
        {
            // arrange
            using var jsonDoc = JsonDocument.Parse(json);

            // act, assert
            Assert.Throws<ArgumentException>(() => new SecurityQuestion(jsonDoc.RootElement, idKey, questionKey));
        }
    }
}
