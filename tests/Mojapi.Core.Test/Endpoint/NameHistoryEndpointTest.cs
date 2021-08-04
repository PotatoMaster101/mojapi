using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint;
using Mojapi.Core.Error;
using Mojapi.Core.Response;
using Xunit;

namespace Mojapi.Core.Test.Endpoint
{
    /// <summary>
    /// Unit test for <see cref="NameHistoryEndpoint"/>.
    /// </summary>
    public class NameHistoryEndpointTest
    {
        [Fact]
        public void Constructor_Sets_Members()
        {
            // arrange, act
            var endpoint = new NameHistoryEndpoint("cb2671d590b84dfe9b1c73683d451d1a");

            // assert
            Assert.Equal("https://api.mojang.com/user/profiles/cb2671d590b84dfe9b1c73683d451d1a/names", endpoint.Address);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_Throws_OnInvalidUuid(string uuid)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new NameHistoryEndpoint(uuid));
        }

        [Theory, ClassData(typeof(RequestData))]
        public async Task Request_Returns_CorrectResponse(string uuid, NameHistoryResponse.NameHistory[] expected)
        {
            // arrange
            var endpoint = new NameHistoryEndpoint(uuid);

            // act
            var response = await endpoint.Request();

            // assert
            Assert.Equal(HttpStatusCode.OK, response.Status);
            Assert.True(response.Data.Length > 0);
            Assert.Equal(expected.Length, response.History.Count);
            foreach (var (name, timestamp) in expected)
            {
                var check = response.History.First(x => x.Name == name);
                Assert.Equal(timestamp, check.Timestamp);
            }
        }

        [Theory]
        [InlineData("000000000000000000000000000000000000")]
        [InlineData("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")]
        public async Task Request_Throws_OnInvalidUuid(string uuid)
        {
            // arrange
            var endpoint = new NameHistoryEndpoint(uuid);

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(async () => await endpoint.Request());
        }

        /// <summary>
        /// Test data for <see cref="NameHistoryEndpointTest.Request_Returns_CorrectResponse"/>.
        /// </summary>
        private class RequestData : IEnumerable<object[]>
        {
            private static readonly NameHistoryResponse.NameHistory Entry1 = new("PotatoMaster101", 0);
            private static readonly NameHistoryResponse.NameHistory Entry2 = new("UltiCheeseMan", 0);
            private static readonly NameHistoryResponse.NameHistory Entry3 = new("_UltiCheeseGirl_", 1456390735000);
            private readonly List<object[]> _data = new()
            {
                new object[] {"cb2671d590b84dfe9b1c73683d451d1a", new[] {Entry1}},
                new object[] {"009bb32764124367a4844185ff0b841c", new[] {Entry2, Entry3}}
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
        }
    }
}
