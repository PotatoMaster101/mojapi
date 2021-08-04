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
    /// Represents the player name change endpoint.
    /// </summary>
    public class ChangeNameEndpoint : BaseAuthenticatedEndpoint<ChangeNameResponse>
    {
        /// <summary>
        /// The change name endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.minecraftservices.com/minecraft/profile/name/{0}";

        /// <summary>
        /// Constructs a new instance of <see cref="ChangeNameEndpoint"/>.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="newName">The new name to change to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="accessToken"/> or <paramref name="newName"/> is <see langword="null"/> or empty.</exception>
        public ChangeNameEndpoint(string accessToken, string newName)
            : base(string.Format(EndpointUrl, newName), accessToken)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("Invalid new name", nameof(newName));
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<ChangeNameResponse> Request()
        {
            var response = await RequestSender.SendPutRequest(this);
            if (response.Status != HttpStatusCode.OK)
                throw new InvalidResponseException(response.Data, response.Status);

            using var json = JsonDocument.Parse(response.Data);
            var root = json.RootElement;
            return new ChangeNameResponse
            {
                Data = response.Data,
                Status = response.Status,
                ChangedName = root.GetProperty("name").GetString(),
                Uuid = root.GetProperty("skins")[0].GetProperty("id").GetString(),
                Skin = new Skin(root.GetProperty("skins")[0])
            };
        }
    }
}
