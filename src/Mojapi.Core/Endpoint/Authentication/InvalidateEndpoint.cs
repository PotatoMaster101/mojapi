using System;
using System.Net;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Error;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint.Authentication
{
    /// <summary>
    /// Represents the invalidate endpoint.
    /// </summary>
    public class InvalidateEndpoint : BaseEndpoint<BaseResponse>
    {
        /// <summary>
        /// The invalidate endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://authserver.mojang.com/invalidate";

        /// <summary>
        /// Constructs a new instance of <see cref="InvalidateEndpoint"/>.
        /// </summary>
        /// <param name="token">The access and client token.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="token"/> is <see langword="null"/>.</exception>
        public InvalidateEndpoint(TokenPair token)
            : base(EndpointUrl)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            PostData = $@"{{{token.ToJsonString()}}}";
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<BaseResponse> Request()
        {
            var response = await RequestSender.SendPostRequest(this);
            if (response.Status != HttpStatusCode.NoContent)
                throw new InvalidResponseException(response.Data, response.Status);
            return response;
        }
    }
}
