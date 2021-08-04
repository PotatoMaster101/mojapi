using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Error;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint
{
    /// <summary>
    /// Represents the player profile endpoint.
    /// </summary>
    public class ProfileEndpoint : BaseEndpoint<ProfileResponse>
    {
        /// <summary>
        /// The player profile endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://sessionserver.mojang.com/session/minecraft/profile/{0}";

        /// <summary>
        /// Gets whether the request should be unsigned.
        /// </summary>
        /// <value>Whether the request should be unsigned.</value>
        public bool Unsigned { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="ProfileEndpoint"/>.
        /// </summary>
        /// <param name="uuid">The UUID to query the profile.</param>
        /// <param name="unsigned">Whether the request should be unsigned.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="uuid"/> is <see langword="null"/> or empty.</exception>
        public ProfileEndpoint(string uuid, bool unsigned = true)
            : base(string.Format(EndpointUrl, uuid))
        {
            if (string.IsNullOrEmpty(uuid))
                throw new ArgumentException("Invalid UUID", nameof(uuid));

            Unsigned = unsigned;
            if (!unsigned)
                Address += @"?unsigned=false";
        }

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
                Player = new PlayerInfo(root),
                Texture = new ProfileResponse.TextureProperty(root.GetProperty("properties")[0])
            };
        }
    }
}
