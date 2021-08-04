using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Mojapi.Core.Error;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint
{
    /// <summary>
    /// Represents the name history endpoint.
    /// </summary>
    public class NameHistoryEndpoint : BaseEndpoint<NameHistoryResponse>
    {
        /// <summary>
        /// The name history endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.mojang.com/user/profiles/{0}/names";

        /// <summary>
        /// Constructs a new instance of <see cref="NameHistoryEndpoint"/>.
        /// </summary>
        /// <param name="uuid">The UUID to query the history.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="uuid"/> is <see langword="null"/> or empty.</exception>
        public NameHistoryEndpoint(string uuid)
            : base(string.Format(EndpointUrl, uuid))
        {
            if (string.IsNullOrEmpty(uuid))
                throw new ArgumentException("Invalid UUID", nameof(uuid));
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<NameHistoryResponse> Request()
        {
            var response = await RequestSender.SendGetRequest(this);
            if (response.Status != HttpStatusCode.OK)
                throw new InvalidResponseException(response.Data, response.Status);

            var history = new NameHistoryResponse
            {
                Data = response.Data,
                Status = response.Status
            };
            using var json = JsonDocument.Parse(response.Data);
            var jsonArr = json.RootElement;
            for (var i = 0; i < jsonArr.GetArrayLength(); i++)
                history.History.Add(GetNameHistory(jsonArr[i]));
            return history;
        }

        /// <summary>
        /// Returns a name history entry from the given JSON data.
        /// </summary>
        /// <param name="json">The JSON data containing the name history.</param>
        /// <returns>The name history entry.</returns>
        private static NameHistoryResponse.NameHistory GetNameHistory(JsonElement json)
        {
            var checkTime = json.TryGetProperty("changedToAt", out var time);
            return new NameHistoryResponse.NameHistory(json.GetProperty("name").GetString(), checkTime ? time.GetInt64() : 0);
        }
    }
}
