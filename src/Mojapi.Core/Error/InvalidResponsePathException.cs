using System.Net;

namespace Mojapi.Core.Error
{
    /// <summary>
    /// Represents an error with the API response which contains an error path.
    /// </summary>
    public class InvalidResponsePathException : InvalidResponseException
    {
        /// <summary>
        /// The key for retrieving the error path.
        /// </summary>
        private const string ErrorPathKey = "path";

        /// <summary>
        /// Gets the error path.
        /// </summary>
        /// <value>The error path.</value>
        public string ErrorPath { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="InvalidResponsePathException"/>.
        /// </summary>
        public InvalidResponsePathException() {}

        /// <summary>
        /// Constructs a new instance of <see cref="InvalidResponsePathException"/>.
        /// </summary>
        /// <param name="json">The JSON string containing the error.</param>
        /// <param name="status">The HTTP status code.</param>
        public InvalidResponsePathException(string json, HttpStatusCode status = HttpStatusCode.BadRequest)
            : base(json, status)
        {
            try
            {
                var checkPath = ErrorJson.TryGetProperty(ErrorPathKey, out var path);
                ErrorPath = checkPath ? path.GetString() : null;
            }
            catch
            {
                // ignored
            }
        }
    }
}
