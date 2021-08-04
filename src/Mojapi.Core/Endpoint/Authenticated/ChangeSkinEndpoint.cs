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
    /// Represents the player skin change endpoint.
    /// </summary>
    public class ChangeSkinEndpoint : BaseAuthenticatedEndpoint<ChangeSkinResponse>
    {
        /// <summary>
        /// The skin change endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.minecraftservices.com/minecraft/profile/skins";

        /// <summary>
        /// Constructs a new instance of <see cref="ChangeSkinEndpoint"/>.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="skin">The new skin to change to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="accessToken"/> is <see langword="null"/> or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="skin"/> is <see langword="null"/>.</exception>
        public ChangeSkinEndpoint(string accessToken, Skin skin)
            : base(EndpointUrl, accessToken)
        {
            if (skin is null)
                throw new ArgumentNullException(nameof(skin));

            PostData = $@"{{{skin.ToJsonString()}}}";
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<ChangeSkinResponse> Request()
        {
            var response = await RequestSender.SendPostRequest(this);
            if (response.Status != HttpStatusCode.OK)
                throw new InvalidResponseException(response.Data, response.Status);

            using var json = JsonDocument.Parse(response.Data);
            var root = json.RootElement;
            return new ChangeSkinResponse
            {
                Data = response.Data,
                Status = response.Status,
                ChangedSkin = new Skin(root.GetProperty("skins")[0]),
            };
        }
    }
}
