using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Error;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint
{
    /// <summary>
    /// Represents the usernames to UUIDs endpoint. This endpoint accepts max 10 names at a time.
    /// </summary>
    public class MultipleUuidEndpoint : BaseEndpoint<MultipleUuidResponse>
    {
        /// <summary>
        /// The usernames to UUIDs endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.mojang.com/profiles/minecraft";

        /// <summary>
        /// Constructs a new instance of <see cref="MultipleUuidEndpoint"/>.
        /// </summary>
        /// <param name="usernames">The list of usernames to query UUID.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="usernames"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <see cref="usernames"/> contains more than 10 names.</exception>
        public MultipleUuidEndpoint(IEnumerable<string> usernames)
            : base(EndpointUrl)
        {
            if (usernames is null)
                throw new ArgumentNullException(nameof(usernames));

            var usernameList = usernames.ToList();
            if (usernameList.Count > 10)
                throw new ArgumentException("Too many usernames", nameof(usernames));

            PostData = "[\"" + string.Join("\",\"", usernameList) + "\"]";
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<MultipleUuidResponse> Request()
        {
            var response = await RequestSender.SendPostRequest(this);
            if (response.Status != HttpStatusCode.OK)
                throw new InvalidResponseException(response.Data, response.Status);

            var uuids = new MultipleUuidResponse
            {
                Data = response.Data,
                Status = response.Status
            };
            using var json = JsonDocument.Parse(response.Data);
            var jsonArr = json.RootElement;
            for (var i = 0; i < jsonArr.GetArrayLength(); i++)
                uuids.Players.Add(new PlayerInfo(jsonArr[i]));
            return uuids;
        }
    }
}
