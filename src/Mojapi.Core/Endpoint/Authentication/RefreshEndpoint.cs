using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Error;
using Mojapi.Core.Response.Authentication;

namespace Mojapi.Core.Endpoint.Authentication
{
    /// <summary>
    /// Represents the refresh endpoint.
    /// </summary>
    public class RefreshEndpoint : BaseEndpoint<RefreshResponse>
    {
        /// <summary>
        /// The refresh endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://authserver.mojang.com/refresh";

        /// <summary>
        /// Constructs a new instance of <see cref="RefreshEndpoint"/>.
        /// </summary>
        /// <param name="token">The access and client token.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="token"/> is <see langword="null"/>.</exception>
        public RefreshEndpoint(TokenPair token)
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
        public override async Task<RefreshResponse> Request()
        {
            var response = await RequestSender.SendPostRequest(this);
            if (response.Status != HttpStatusCode.OK)
                throw new InvalidResponseCauseException(response.Data, response.Status);

            using var json = JsonDocument.Parse(response.Data);
            var root = json.RootElement;
            return new RefreshResponse
            {
                Token = new TokenPair(root),
                SelectedProfile = new Player(root.GetProperty("selectedProfile"))
            };
        }
    }
}
