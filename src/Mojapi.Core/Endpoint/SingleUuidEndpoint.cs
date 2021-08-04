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
    /// Represents the username to UUID endpoint.
    /// </summary>
    public class SingleUuidEndpoint : BaseEndpoint<SingleUuidResponse>
    {
        /// <summary>
        /// The username to UUID endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.mojang.com/users/profiles/minecraft/{0}";

        /// <summary>
        /// Constructs a new instance of <see cref="SingleUuidEndpoint"/>.
        /// </summary>
        /// <param name="username">The username to query the UUID.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> is <see langword="null"/> or empty.</exception>
        public SingleUuidEndpoint(string username)
            : base(string.Format(EndpointUrl, username))
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Invalid username", nameof(username));
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<SingleUuidResponse> Request()
        {
            var response = await RequestSender.SendGetRequest(this);
            if (response.Status != HttpStatusCode.OK)
                throw new InvalidResponseException(response.Data, response.Status);

            using var json = JsonDocument.Parse(response.Data);
            return new SingleUuidResponse
            {
                Data = response.Data,
                Status = response.Status,
                Player = new PlayerInfo(json.RootElement)
            };
        }
    }
}
