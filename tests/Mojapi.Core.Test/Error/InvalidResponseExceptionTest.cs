using System.Net;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Error
{
    /// <summary>
    /// Unit test for <see cref="InvalidResponseException"/>.
    /// </summary>
    public class InvalidResponseExceptionTest
    {
        [Fact]
        public void DefaultConstructor_Sets_Null()
        {
            // arrange, act
            var ex = new InvalidResponseException();

            // assert
            Assert.Null(ex.ErrorName);
            Assert.Null(ex.ErrorMessage);
            Assert.Equal(HttpStatusCode.BadRequest, ex.Status);
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData(@"{not a json}", null, null)]
        [InlineData(@"{""error"": ""some error"", ""errorMessage"": ""some error message""}", "some error", "some error message")]
        [InlineData(@"{""foo"": ""foobar"", ""bar"": ""foobar""}", null, null)]
        [InlineData(@"{""error"": ""some error"", ""bar"": ""foobar""}", "some error", null)]
        [InlineData(@"{""foo"": ""foobar"", ""errorMessage"": ""some error message""}", null, "some error message")]
        public void StringConstructor_Sets_Members(string json, string name, string message)
        {
            // arrange, act
            var ex = new InvalidResponseException(json);

            // assert
            Assert.Equal(name, ex.ErrorName);
            Assert.Equal(message, ex.ErrorMessage);
            Assert.Equal(HttpStatusCode.BadRequest, ex.Status);
        }
    }
}
