using System;
using System.Net;
using System.Threading.Tasks;
using Mojapi.Core.Error;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint.Authenticated
{
    /// <summary>
    /// Represents the reset skin endpoint.
    /// </summary>
    public class ResetSkinEndpoint : BaseAuthenticatedEndpoint<BaseResponse>
    {
        /// <summary>
        /// The reset skin endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.mojang.com/user/profile/{0}/skin";

        /// <summary>
        /// Constructs a new instance of <see cref="ResetSkinEndpoint"/>.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="uuid">The player UUID.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="accessToken"/> or <paramref name="uuid"/> is <see langword="null"/> or empty.</exception>
        public ResetSkinEndpoint(string accessToken, string uuid)
            : base(string.Format(EndpointUrl, uuid), accessToken)
        {
            if (string.IsNullOrEmpty(uuid))
                throw new ArgumentException("Invalid UUID", nameof(uuid));
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<BaseResponse> Request()
        {
            var response = await RequestSender.SendDeleteRequest(this);
            if (response.Status != HttpStatusCode.NoContent)
                throw new InvalidResponseException(response.Data, response.Status);
            return response;
        }
    }
}
