using System;
using System.Threading.Tasks;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint.Authenticated
{
    /// <summary>
    /// Represents the base class for all authenticated endpoints.
    /// </summary>
    /// <typeparam name="TResponse">The endpoint response type.</typeparam>
    public abstract class BaseAuthenticatedEndpoint<TResponse> : BaseEndpoint<TResponse>
        where TResponse : BaseResponse
    {
        /// <summary>
        /// Constructs a new instance of <see cref="BaseAuthenticatedEndpoint{TResponse}"/>.
        /// </summary>
        /// <param name="address">The endpoint address.</param>
        /// <param name="accessToken">The access token.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="accessToken"/> is <see langword="null"/> or empty.</exception>
        protected BaseAuthenticatedEndpoint(string address, string accessToken)
            : base(address)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentException("Invalid access token", nameof(accessToken));

            Bearer = accessToken;
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        public abstract override Task<TResponse> Request();
    }
}
