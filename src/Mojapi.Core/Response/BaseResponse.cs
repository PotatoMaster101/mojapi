using System;
using System.Net;

namespace Mojapi.Core.Response
{
    /// <summary>
    /// Represents a response from the endpoint.
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Gets the data from the response.
        /// </summary>
        /// <value>The data from the response.</value>
        public string Data { get; init; }

        /// <summary>
        /// Gets the response status code.
        /// </summary>
        /// <value>The response status code.</value>
        public HttpStatusCode Status { get; init; }

        /// <summary>
        /// Constructs a new instance of <see cref="BaseResponse"/>.
        /// </summary>
        public BaseResponse()
            : this(string.Empty) {}

        /// <summary>
        /// Constructs a new instance of <see cref="BaseResponse"/>.
        /// </summary>
        /// <param name="data">The data of the response.</param>
        /// <param name="status">The response status code.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="data"/> is <see langword="null"/>.</exception>
        public BaseResponse(string data, HttpStatusCode status = HttpStatusCode.OK)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Status = status;
        }
    }
}
