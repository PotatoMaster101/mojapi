using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Endpoint.Authenticated;
using Mojapi.Core.Error;
using Xunit;

namespace Mojapi.Core.Test.Endpoint.Authenticated
{
    /// <summary>
    /// Unit test for <see cref="SecurityAnswerEndpoint"/>.
    /// </summary>
    public class SecurityAnswerEndpointTest
    {
        [Theory, ClassData(typeof(ConstructorData))]
        public void Constructor_Sets_Members(string access, SecurityAnswer[] answers, string post)
        {
            // arrange, act
            var endpoint = new SecurityAnswerEndpoint(access, answers);

            // assert
            Assert.Equal(access, endpoint.Bearer);
            Assert.Equal(post, endpoint.PostData);
        }

        [Fact]
        public void Constructor_Throws_OnInvalidParams()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new SecurityAnswerEndpoint("", null));
            Assert.Throws<ArgumentNullException>(() => new SecurityAnswerEndpoint("good", null));
        }

        [Theory]
        [InlineData("x")]
        public async Task Request_Throws_OnInvalidBearer(string access)
        {
            // arrange
            var answers = new[] {new SecurityAnswer(1, "a"), new SecurityAnswer(2, "b"), new SecurityAnswer(3, "c")};
            var endpoint = new SecurityAnswerEndpoint(access, answers);

            // act, assert
            await Assert.ThrowsAsync<InvalidResponseException>(async () => await endpoint.Request());
        }

        /// <summary>
        /// Test data for constructors.
        /// </summary>
        private class ConstructorData : IEnumerable<object[]>
        {
            private readonly SecurityAnswer _entry1 = new(1, "abc");
            private readonly SecurityAnswer _entry2 = new(2, "def");
            private readonly SecurityAnswer _entry3 = new(3, "ghi");

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {"token1", new[] {_entry1}, @"[{""id"":1,""answer"":""abc""}]"};
                yield return new object[] {"token2", new[] {_entry2, _entry3}, @"[{""id"":2,""answer"":""def""},{""id"":3,""answer"":""ghi""}]"};
                yield return new object[] {"token3", Array.Empty<SecurityAnswer>(), @"[{}]"};
            }
        }
    }
}
