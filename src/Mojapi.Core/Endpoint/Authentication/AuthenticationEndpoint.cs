using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Error;
using Mojapi.Core.Response.Authentication;

namespace Mojapi.Core.Endpoint.Authentication
{
    /// <summary>
    /// Represents the authentication endpoint.
    /// </summary>
    public class AuthenticationEndpoint : BaseEndpoint<AuthenticationResponse>
    {
        /// <summary>
        /// The authentication endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://authserver.mojang.com/authenticate";

        /// <summary>
        /// The agent in the payload.
        /// </summary>
        private const string Agent = @"""agent"":{""name"":""Minecraft"",""version"":1}";

        /// <summary>
        /// Constructs a new instance of <see cref="AuthenticationEndpoint"/>.
        /// </summary>
        /// <param name="credentials">The user credentials.</param>
        /// <param name="requestUser">Whether to request user in the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="credentials"/> is <see langword="null"/>.</exception>
        public AuthenticationEndpoint(Credentials credentials, bool requestUser = false)
            : base(EndpointUrl)
        {
            if (credentials is null)
                throw new ArgumentNullException(nameof(credentials));

            var req = requestUser.ToString().ToLower();
            PostData = $@"{{{Agent},{credentials.ToJsonString()},""clientToken"":""{Guid.NewGuid()}"",""requestUser"":{req}}}";
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<AuthenticationResponse> Request()
        {
            var response = await RequestSender.SendPostRequest(this);
            if (response.Status != HttpStatusCode.OK)
                throw new InvalidResponseCauseException(response.Data, response.Status);

            using var json = JsonDocument.Parse(response.Data);
            var root = json.RootElement;
            var checkUser = root.TryGetProperty("user", out var user);
            var checkSelectedProfile = root.TryGetProperty("selectedProfile", out var selectedProfile);
            return new AuthenticationResponse
            {
                Data = response.Data,
                Status = response.Status,
                Player = checkUser ? new Player(user, "username") : null,
                Token = new TokenPair(root),
                AvailableProfiles = GetProfiles(root),
                SelectedProfile = checkSelectedProfile ? new Player(selectedProfile) : null
            };
        }

        /// <summary>
        /// Returns the list of available profiles from the JSON data.
        /// </summary>
        /// <param name="json">The JSON data to extract profiles.</param>
        /// <returns>The list of profiles from the JSON data.</returns>
        private static IEnumerable<Player> GetProfiles(JsonElement json)
        {
            var list = new List<Player>();
            var profs = json.GetProperty("availableProfiles");
            for (var i = 0; i < profs.GetArrayLength(); i++)
                list.Add(new Player(profs[i]));
            return list;
        }
    }
}
