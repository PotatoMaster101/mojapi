using System;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint.Authenticated;
using Mojapi.Core.Response;
using Xunit;

namespace Mojapi.Core.Test.Endpoint.Authenticated
{
    /// <summary>
    /// Unit test for <see cref="BaseAuthenticatedEndpoint{TResponse}"/>.
    /// </summary>
    public class BaseAuthenticatedEndpointTest
    {
        [Theory]
        [InlineData("abc")]
        public void Constructor_Sets_Members(string access)
        {
            // arrange, act
            var endpoint = new TestAuthenticatedEndpoint(access);

            // assert
            Assert.Equal(access, endpoint.Bearer);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_Throws_OnInvalidParams(string access)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new TestAuthenticatedEndpoint(access));
        }

        /// <summary>
        /// Fake class used for testing.
        /// </summary>
        private class TestAuthenticatedEndpoint : BaseAuthenticatedEndpoint<BaseResponse>
        {
            public TestAuthenticatedEndpoint(string accessToken)
                : base("fake address", accessToken) {}

            public override Task<BaseResponse> Request() => throw new NotImplementedException();
        }
    }
}
