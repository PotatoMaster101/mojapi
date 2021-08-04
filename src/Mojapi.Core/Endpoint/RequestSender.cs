using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint
{
    /// <summary>
    /// Sends HTTP requests to an endpoint.
    /// </summary>
    public static class RequestSender
    {
        /// <summary>
        /// The HTTP client used for requests.
        /// </summary>
        public static readonly HttpClient Client = new();

        /// <summary>
        /// Sends a GET request to the API endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint to send the request.</param>
        /// <typeparam name="TResponse">The response type from the endpoint.</typeparam>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="endpoint"/> is <see langword="null"/>.</exception>
        public static async Task<BaseResponse> SendGetRequest<TResponse>(BaseEndpoint<TResponse> endpoint)
            where TResponse : BaseResponse
        {
            if (endpoint is null)
                throw new ArgumentNullException(nameof(endpoint));

            using var msg = GetRequestMessage(endpoint, HttpMethod.Get);
            return await SendRequest(msg);
        }

        /// <summary>
        /// Sends a POST request to the API endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint to send the request.</param>
        /// <typeparam name="TResponse">The response type from the endpoint.</typeparam>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="endpoint"/> is <see langword="null"/>.</exception>
        public static async Task<BaseResponse> SendPostRequest<TResponse>(BaseEndpoint<TResponse> endpoint)
            where TResponse : BaseResponse
        {
            if (endpoint is null)
                throw new ArgumentNullException(nameof(endpoint));

            using var msg = GetRequestMessage(endpoint, HttpMethod.Post);
            return await SendRequest(msg);
        }

        /// <summary>
        /// Sends a POST request to the API endpoint as if it is sent from a form.
        /// </summary>
        /// <param name="endpoint">The endpoint to send the request.</param>
        /// <param name="formPost">The form post data to use. This will overwrite <see cref="BaseEndpoint{TResponse}.PostData"/>.</param>
        /// <typeparam name="TResponse">The response type from the endpoint.</typeparam>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="endpoint"/> is <see langword="null"/>.</exception>
        public static async Task<BaseResponse> SendFormPostRequest<TResponse>(BaseEndpoint<TResponse> endpoint, HttpContent formPost)
            where TResponse : BaseResponse
        {
            if (endpoint is null)
                throw new ArgumentNullException(nameof(endpoint));
            if (formPost is null)
                throw new ArgumentNullException(nameof(formPost));

            using var msg = GetRequestMessage(endpoint, HttpMethod.Post);
            msg.Content = formPost;
            return await SendRequest(msg);
        }

        /// <summary>
        /// Sends a PUT request to the API endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint to send the request.</param>
        /// <typeparam name="TResponse">The response type from the endpoint.</typeparam>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="endpoint"/> is <see langword="null"/>.</exception>
        public static async Task<BaseResponse> SendPutRequest<TResponse>(BaseEndpoint<TResponse> endpoint)
            where TResponse : BaseResponse
        {
            if (endpoint is null)
                throw new ArgumentNullException(nameof(endpoint));

            using var msg = GetRequestMessage(endpoint, HttpMethod.Put);
            return await SendRequest(msg);
        }

        /// <summary>
        /// Sends a DELETE request to the API endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint to send the request.</param>
        /// <typeparam name="TResponse">The response type from the endpoint.</typeparam>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="endpoint"/> is <see langword="null"/>.</exception>
        public static async Task<BaseResponse> SendDeleteRequest<TResponse>(BaseEndpoint<TResponse> endpoint)
            where TResponse : BaseResponse
        {
            if (endpoint is null)
                throw new ArgumentNullException(nameof(endpoint));

            using var msg = GetRequestMessage(endpoint, HttpMethod.Delete);
            return await SendRequest(msg);
        }

        /// <summary>
        /// Sends a request using the given <see cref="HttpRequestMessage"/>.
        /// </summary>
        /// <param name="msg">The endpoint and message to send.</param>
        /// <returns>A <see cref="BaseResponse"/> that contains the request response.</returns>
        private static async Task<BaseResponse> SendRequest(HttpRequestMessage msg)
        {
            var response = await Client.SendAsync(msg);
            return new BaseResponse(await response.Content.ReadAsStringAsync(), response.StatusCode);
        }

        /// <summary>
        /// Returns a <see cref="HttpRequestMessage"/> with the content set.
        /// </summary>
        /// <param name="endpoint">The endpoint of the request.</param>
        /// <param name="method">The HTTP method of the request.</param>
        /// <returns>A <see cref="HttpRequestMessage"/> with the content set.</returns>
        private static HttpRequestMessage GetRequestMessage<TResponse>(BaseEndpoint<TResponse> endpoint, HttpMethod method)
            where TResponse : BaseResponse
        {
            var msg = new HttpRequestMessage(method, endpoint.Address);
            if (endpoint.Bearer is not null)        // set auth token
                msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", endpoint.Bearer);
            if (method == HttpMethod.Post && endpoint.PostData is not null)     // set JSON content type if POST data
                msg.Content = new StringContent(endpoint.PostData, Encoding.Default, endpoint.ContentType);
            return msg;
        }
    }
}
