using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Error
{
    /// <summary>
    /// Unit test for <see cref="InvalidResponseCauseException"/>.
    /// </summary>
    public class InvalidResponseCauseExceptionTest
    {
        [Fact]
        public void DefaultConstructor_Sets_Null()
        {
            // arrange, act
            var ex = new InvalidResponseCauseException();

            // assert
            Assert.Null(ex.ErrorCause);
        }

        [Theory]
        [InlineData(@"{not a json}", null)]
        [InlineData(@"{""cause"":""abc""}", "abc")]
        [InlineData(@"{""invalid"":""abc""}", null)]
        public void StringConstructor_Sets_Members(string json, string cause)
        {
            // arrange, act
            var ex = new InvalidResponseCauseException(json);

            // assert
            Assert.Equal(cause, ex.ErrorCause);
        }
    }
}
