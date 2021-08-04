using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Error
{
    /// <summary>
    /// Unit test for <see cref="InvalidResponsePathException"/>.
    /// </summary>
    public class InvalidResponsePathExceptionTest
    {
        [Fact]
        public void DefaultConstructor_Sets_Null()
        {
            // arrange, act
            var ex = new InvalidResponsePathException();

            // assert
            Assert.Null(ex.ErrorPath);
        }

        [Theory]
        [InlineData(@"{not a json}", null)]
        [InlineData(@"{""path"":""abc""}", "abc")]
        [InlineData(@"{""invalid"":""abc""}", null)]
        public void StringConstructor_Sets_Members(string json, string path)
        {
            // arrange, act
            var ex = new InvalidResponsePathException(json);

            // assert
            Assert.Equal(path, ex.ErrorPath);
        }
    }
}
