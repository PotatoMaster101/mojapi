using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Mojapi.Core.Error;
using Mojapi.Core.Response.Authenticated;

namespace Mojapi.Core.Endpoint.Authenticated
{
    /// <summary>
    /// Represents the player name availability endpoint.
    /// </summary>
    public class NameAvailabilityEndpoint : BaseAuthenticatedEndpoint<NameAvailabilityResponse>
    {
        /// <summary>
        /// The name availability endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.minecraftservices.com/minecraft/profile/name/{0}/available";

        /// <summary>
        /// Constructs a new instance of <see cref="NameAvailabilityEndpoint"/>.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="name">The username to query.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="accessToken"/> or <paramref name="name"/> is <see langword="null"/> or empty.</exception>
        public NameAvailabilityEndpoint(string accessToken, string name)
            : base(string.Format(EndpointUrl, name), accessToken)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Invalid username", nameof(name));
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<NameAvailabilityResponse> Request()
        {
            var response = await RequestSender.SendGetRequest(this);
            if (response.Status != HttpStatusCode.OK)
                throw new InvalidResponseException(response.Data, response.Status);

            using var json = JsonDocument.Parse(response.Data);
            return new NameAvailabilityResponse
            {
                Data = response.Data,
                Status = response.Status,
                NameStatus = json.RootElement.GetProperty("status").GetString()
            };
        }
    }
}
