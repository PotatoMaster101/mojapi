using System;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint;
using Mojapi.Core.Response;
using Xunit;

namespace Mojapi.Core.Test.Endpoint
{
    /// <summary>
    /// Unit test for <see cref="BaseEndpoint{TResponse}"/>.
    /// </summary>
    public class BaseEndpointTest
    {
        [Fact]
        public void Constructor_Sets_Members()
        {
            // arrange, act
            var endpoint = new TestEndpoint("test.com");

            // assert
            Assert.Equal("test.com", endpoint.Address);
        }

        [Fact]
        public void Constructor_Throws_OnNullParams()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentNullException>(() => new TestEndpoint(null));
        }

        /// <summary>
        /// A custom endpoint used for testing.
        /// </summary>
        private class TestEndpoint : BaseEndpoint<BaseResponse>
        {
            public TestEndpoint(string address)
                : base(address) {}

            public override Task<BaseResponse> Request() => throw new NotImplementedException();
        }
    }
}
