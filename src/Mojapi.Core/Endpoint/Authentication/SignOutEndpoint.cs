using System;
using System.Net;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Error;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint.Authentication
{
    /// <summary>
    /// Represents the sign out endpoint.
    /// </summary>
    public class SignOutEndpoint : BaseEndpoint<BaseResponse>
    {
        /// <summary>
        /// The sign out endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://authserver.mojang.com/signout";

        /// <summary>
        /// Constructs a new instance of <see cref="SignOutEndpoint"/>.
        /// </summary>
        /// <param name="credentials">The user credentials.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="credentials"/> is <see langword="null"/>.</exception>
        public SignOutEndpoint(Credentials credentials)
            : base(EndpointUrl)
        {
            if (credentials is null)
                throw new ArgumentNullException(nameof(credentials));

            PostData = $@"{{{credentials.ToJsonString()}}}";
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
