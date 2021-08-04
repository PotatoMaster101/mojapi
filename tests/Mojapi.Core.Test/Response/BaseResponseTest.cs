using System;
using System.Net;
using Mojapi.Core.Response;
using Xunit;

namespace Mojapi.Core.Test.Response
{
    /// <summary>
    /// Unit test for <see cref="BaseResponse"/>.
    /// </summary>
    public class BaseResponseTest
    {
        [Fact]
        public void DefaultConstructor_Sets_CorrectMembers()
        {
            // arrange, act
            var response = new BaseResponse();

            // assert
            Assert.Equal(string.Empty, response.Data);
            Assert.Equal(HttpStatusCode.OK, response.Status);
        }

        [Theory]
        [InlineData("", HttpStatusCode.OK)]
        [InlineData("test", HttpStatusCode.Forbidden)]
        public void Constructor_Sets_CorrectMembers(string data, HttpStatusCode status)
        {
            // arrange, act
            var response = new BaseResponse(data, status);

            // assert
            Assert.Equal(data, response.Data);
            Assert.Equal(status, response.Status);
        }

        [Fact]
        public void Constructor_Throws_OnNullParams()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentNullException>(() => new BaseResponse(null));
        }
    }
}
