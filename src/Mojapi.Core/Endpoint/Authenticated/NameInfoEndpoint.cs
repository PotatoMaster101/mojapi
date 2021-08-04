using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Mojapi.Core.Error;
using Mojapi.Core.Response.Authenticated;

namespace Mojapi.Core.Endpoint.Authenticated
{
    /// <summary>
    /// Represents the player name information endpoint.
    /// </summary>
    public class NameInfoEndpoint : BaseAuthenticatedEndpoint<NameInfoResponse>
    {
        /// <summary>
        /// The name information endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.minecraftservices.com/minecraft/profile/namechange";

        /// <summary>
        /// Constructs a new instance of <see cref="NameInfoEndpoint"/>.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="accessToken"/> is <see langword="null"/> or empty.</exception>
        public NameInfoEndpoint(string accessToken)
            : base(EndpointUrl, accessToken) {}

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<NameInfoResponse> Request()
        {
            var response = await RequestSender.SendGetRequest(this);
            if (response.Status != HttpStatusCode.OK)
                throw new InvalidResponseException(response.Data, response.Status);

            using var json = JsonDocument.Parse(response.Data);
            var root = json.RootElement;
            var checkChange = root.TryGetProperty("changedAt", out var changedAt);
            return new NameInfoResponse
            {
                Data = response.Data,
                Status = response.Status,
                ChangedAt = checkChange ? changedAt.GetString() : null,
                CreatedAt = root.GetProperty("createdAt").GetString(),
                NameChangeAllowed = root.GetProperty("nameChangeAllowed").GetBoolean()
            };
        }
    }
}
