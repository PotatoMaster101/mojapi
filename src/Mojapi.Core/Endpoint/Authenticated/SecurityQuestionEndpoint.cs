using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Error;
using Mojapi.Core.Response.Authenticated;

namespace Mojapi.Core.Endpoint.Authenticated
{
    /// <summary>
    /// Represents the security question endpoint.
    /// </summary>
    public class SecurityQuestionEndpoint : BaseAuthenticatedEndpoint<SecurityQuestionResponse>
    {
        /// <summary>
        /// The security question endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.mojang.com/user/security/challenges";

        /// <summary>
        /// Constructs a new instance of <see cref="SecurityQuestionEndpoint"/>.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="accessToken"/> is <see langword="null"/> or empty.</exception>
        public SecurityQuestionEndpoint(string accessToken)
            : base(EndpointUrl, accessToken) {}

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<SecurityQuestionResponse> Request()
        {
            var response = await RequestSender.SendGetRequest(this);
            if (response.Status != HttpStatusCode.OK)
                throw new InvalidResponseException(response.Data, response.Status);

            var questions = new Dictionary<long, SecurityQuestion>();
            using var jsonDoc = JsonDocument.Parse(response.Data);
            var root = jsonDoc.RootElement;
            for (var i = 0; i < root.GetArrayLength(); i++)
                questions.Add(root[i].GetProperty("answer").GetProperty("id").GetInt64(), new SecurityQuestion(root[i].GetProperty("question")));

            return new SecurityQuestionResponse
            {
                Data = response.Data,
                Status = response.Status,
                Questions = questions
            };
        }
    }
}
