using System;
using System.Net;
using System.Threading.Tasks;
using Mojapi.Core.Error;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint.Authenticated
{
    /// <summary>
    /// Represents the secure location endpoint.
    /// </summary>
    public class SecureLocationEndpoint : BaseAuthenticatedEndpoint<BaseResponse>
    {
        /// <summary>
        /// The secure location endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.mojang.com/user/security/location";

        /// <summary>
        /// Constructs a new instance of <see cref="SecureLocationEndpoint"/>.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="accessToken"/> is <see langword="null"/> or empty.</exception>
        public SecureLocationEndpoint(string accessToken)
            : base(EndpointUrl, accessToken) {}

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<BaseResponse> Request()
        {
            var response = await RequestSender.SendGetRequest(this);
            if (response.Status != HttpStatusCode.NoContent)
                throw new InvalidResponseException(response.Data, response.Status);
            return response;
        }
    }
}
