using System;
using System.Threading.Tasks;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint
{
    /// <summary>
    /// Represents an endpoint in the Mojang Api.
    /// </summary>
    /// <typeparam name="TResponse">The response type from the endpoint.</typeparam>
    public abstract class BaseEndpoint<TResponse>
        where TResponse : BaseResponse
    {
        /// <summary>
        /// Gets the endpoint address.
        /// </summary>
        /// <value>The endpoint address.</value>
        public string Address { get; protected init; }

        /// <summary>
        /// Gets the Bearer token.
        /// </summary>
        /// <value>The Bearer token.</value>
        public string Bearer { get; init; }

        /// <summary>
        /// Gets the payload for the POST request.
        /// </summary>
        /// <value>The payload for the POST request.</value>
        public string PostData { get; protected init; }

        /// <summary>
        /// Gets the request content type.
        /// </summary>
        /// <value>The request content type.</value>
        public string ContentType { get; protected init; } = "application/json";

        /// <summary>
        /// Constructs a new instance of <see cref="BaseEndpoint{TResponse}"/>.
        /// </summary>
        /// <param name="address">The address of the endpoint.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="address"/> is <see langword="null"/>.</exception>
        protected BaseEndpoint(string address)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        public abstract Task<TResponse> Request();
    }
}
