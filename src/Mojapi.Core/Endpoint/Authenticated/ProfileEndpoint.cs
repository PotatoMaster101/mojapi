using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Error;
using Mojapi.Core.Response.Authenticated;

namespace Mojapi.Core.Endpoint.Authenticated
{
    /// <summary>
    /// Represents the player profile endpoint.
    /// </summary>
    public class ProfileEndpoint : BaseAuthenticatedEndpoint<ProfileResponse>
    {
        /// <summary>
        /// The profile endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.minecraftservices.com/minecraft/profile";

        /// <summary>
        /// Constructs a new instance of <see cref="ProfileEndpoint"/>.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="accessToken"/> is <see langword="null"/> or empty.</exception>
        public ProfileEndpoint(string accessToken)
            : base(EndpointUrl, accessToken) {}

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<ProfileResponse> Request()
        {
            var response = await RequestSender.SendGetRequest(this);
            if (response.Status != HttpStatusCode.OK)
                throw new InvalidResponseException(response.Data, response.Status);

            using var json = JsonDocument.Parse(response.Data);
            var root = json.RootElement;
            return new ProfileResponse
            {
                Data = response.Data,
                Status = response.Status,
                Player = new Player(root),
                Skin = new Skin(root.GetProperty("skins")[0])
            };
        }
    }
}
