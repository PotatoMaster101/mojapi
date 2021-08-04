using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Endpoint;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint
{
    /// <summary>
    /// Unit test for <see cref="MultipleUuidEndpoint"/>.
    /// </summary>
    public class MultipleUuidEndpointTest
    {
        [Theory]
        [InlineData(@"[""abc"",""def""]", "abc", "def")]
        [InlineData(@"[""abc""]", "abc")]
        [InlineData(@"[""""]")]
        public void Constructor_Sets_Members(string post, params string[] names)
        {
            // arrange, act
            var endpoint = new MultipleUuidEndpoint(names);

            // assert
            Assert.Equal(post, endpoint.PostData);
        }

        [Fact]
        public void Constructor_Throws_OnNullParams()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentNullException>(() => new MultipleUuidEndpoint(null));
        }

        [Theory]
        [InlineData("1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11")]
        public void Constructor_Throws_OnTooManyUsernames(params string[] players)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new MultipleUuidEndpoint(players));
        }

        [Theory, ClassData(typeof(RequestData))]
        public async Task Request_Returns_CorrectResponse(IEnumerable<string> players, PlayerInfo[] expected)
        {
            // arrange
            var endpoint = new MultipleUuidEndpoint(players);

            // act
            var response = await endpoint.Request();

            // assert
            Assert.Equal(HttpStatusCode.OK, response.Status);
            Assert.True(response.Data.Length > 0);
            Assert.Equal(expected.Length, response.Players.Count);
            foreach (var player in expected)
            {
                var check = response.Players.First(x => x.Username == player.Username);
                Assert.Equal(player.Uuid, check.Uuid);
                Assert.Equal(player.Legacy, check.Legacy);
                Assert.Equal(player.Demo, check.Demo);
            }
        }

        [Theory]
        [InlineData]
        [InlineData("ThisUsernameIsWayTooLong123", "ThisUsernameIsWayTooLong456")]
        [InlineData(null, null)]
        [InlineData("")]
        public async Task Request_Throws_OnInvalidUsernames(params string[] players)
        {
            // arrange
            var endpoint = new MultipleUuidEndpoint(players);

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(async () => await endpoint.Request());
        }

        /// <summary>
        /// Test data for <see cref="MultipleUuidEndpointTest.Request_Returns_CorrectResponse"/>.
        /// </summary>
        private class RequestData : IEnumerable<object[]>
        {
            private static readonly PlayerInfo Entry1 = new(JsonDocument.Parse(@"{""name"":""PotatoMaster101"",""id"":""cb2671d590b84dfe9b1c73683d451d1a""}").RootElement);
            private static readonly PlayerInfo Entry2 = new(JsonDocument.Parse(@"{""name"":""Herobrine"",""id"":""f84c6a790a4e45e0879bcd49ebd4c4e2""}").RootElement);
            private static readonly PlayerInfo Entry3 = new(JsonDocument.Parse(@"{""name"":""Notch"",""id"":""069a79f444e94726a5befca90e38aaf5""}").RootElement);
            private readonly List<object[]> _data = new()
            {
                new object[] {new[] {"PotatoMaster101", "Herobrine", "Notch"}, new[] {Entry1, Entry2, Entry3}}
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
        }
    }
}
