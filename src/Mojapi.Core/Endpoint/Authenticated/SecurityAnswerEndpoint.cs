using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Error;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint.Authenticated
{
    /// <summary>
    /// Represents the security answer endpoint.
    /// </summary>
    public class SecurityAnswerEndpoint : BaseAuthenticatedEndpoint<BaseResponse>
    {
        /// <summary>
        /// The security answer endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.mojang.com/user/security/location";

        /// <summary>
        /// Constructs a new instance of <see cref="SecurityAnswerEndpoint"/>.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="answers">The list of answers.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="accessToken"/> is <see langword="null"/> or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="answers"/> is <see langword="null"/>.</exception>
        public SecurityAnswerEndpoint(string accessToken, IEnumerable<SecurityAnswer> answers)
            : base(EndpointUrl, accessToken)
        {
            if (answers is null)
                throw new ArgumentNullException(nameof(answers));

            PostData = $@"[{{{string.Join("},{", answers.Select(x => x.ToJsonString()))}}}]";
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<BaseResponse> Request()
        {
            var response = await RequestSender.SendPostRequest(this);
            if (response.Status != HttpStatusCode.NoContent)
                throw new InvalidResponseException(response.Data, response.Status);
            return response;
        }
    }
}
