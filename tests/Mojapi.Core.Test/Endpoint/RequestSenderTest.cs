using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint;
using Mojapi.Core.Response;
using Xunit;

namespace Mojapi.Core.Test.Endpoint
{
    /// <summary>
    /// Unit test for <see cref="RequestSender"/>.
    /// </summary>
    public class RequestSenderTest
    {
        [Fact]
        public async Task SendGetRequest_Sends_Get()
        {
            // arrange, act
            var response = await RequestSender.SendGetRequest(new TestEndpoint("https://google.com"));

            // assert
            Assert.True(response.Data.Length > 0);
            Assert.Equal(HttpStatusCode.OK, response.Status);
        }

        [Fact]
        public async Task SendGetRequest_Sends_Bearer()
        {
            // arrange
            var endpoint = new TestEndpoint("https://api.minecraftservices.com/minecraft/profile")
            {
                Bearer = "test"
            };

            // act
            var response = await RequestSender.SendGetRequest(endpoint);

            // assert
            Assert.Equal(0, response.Data.Length);
            Assert.Equal(HttpStatusCode.Unauthorized, response.Status);
        }

        [Fact]
        public async Task SendGetRequest_Throws_OnNullParams()
        {
            // arrange, act, assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await RequestSender.SendGetRequest<BaseResponse>(null));
        }

        [Fact]
        public async Task SendPostRequest_Sends_Post()
        {
            // arrange, act
            var response = await RequestSender.SendPostRequest(new TestEndpoint("https://google.com"));

            // assert
            Assert.True(response.Data.Length > 0);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.Status);
        }

        [Fact]
        public async Task SendPostRequest_Throws_OnNullParams()
        {
            // arrange, act, assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await RequestSender.SendPostRequest<BaseResponse>(null));
        }

        [Fact]
        public async Task SendFormPostRequest_Sends_FormPost()
        {
            // arrange
            using var formData = new MultipartFormDataContent
            {
                new StringContent("abc")
            };

            // act
            var response = await RequestSender.SendFormPostRequest(new TestEndpoint("https://google.com"), formData);

            // assert
            Assert.True(response.Data.Length > 0);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.Status);
        }

        [Fact]
        public async Task SendFormPostRequest_Throws_OnNullParams()
        {
            // arrange
            var good = new TestEndpoint("https://google.com");

            // act, assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await RequestSender.SendFormPostRequest(good, null));
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await RequestSender.SendFormPostRequest<BaseResponse>(null, null));
        }

        [Fact]
        public async Task SendPutRequest_Sends_Put()
        {
            // arrange, act
            var response = await RequestSender.SendPutRequest(new TestEndpoint("https://google.com"));

            // assert
            Assert.True(response.Data.Length > 0);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.Status);
        }

        [Fact]
        public async Task SendPutRequest_Throws_OnNullParams()
        {
            // arrange, act, assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await RequestSender.SendPutRequest<BaseResponse>(null));
        }

        [Fact]
        public async Task SendDeleteRequest_Sends_Put()
        {
            // arrange, act
            var response = await RequestSender.SendDeleteRequest(new TestEndpoint("https://google.com"));

            // assert
            Assert.True(response.Data.Length > 0);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.Status);
        }

        [Fact]
        public async Task SendDeleteRequest_Throws_OnNullParams()
        {
            // arrange, act, assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await RequestSender.SendDeleteRequest<BaseResponse>(null));
        }

        /// <summary>
        /// Fake endpoint used for testing.
        /// </summary>
        private class TestEndpoint : BaseEndpoint<BaseResponse>
        {
            public TestEndpoint(string address)
                : base(address) {}

            public override Task<BaseResponse> Request() => throw new NotImplementedException();
        }
    }
}
