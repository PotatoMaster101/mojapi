using System.Net;

namespace Mojapi.Core.Error
{
    /// <summary>
    /// Represents an error with the API response which contains an error cause.
    /// </summary>
    public class InvalidResponseCauseException : InvalidResponseException
    {
        /// <summary>
        /// The key for retrieving the error cause.
        /// </summary>
        private const string ErrorCauseKey = "cause";

        /// <summary>
        /// Gets the error cause.
        /// </summary>
        /// <value>The error cause.</value>
        public string ErrorCause { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="InvalidResponseCauseException"/>.
        /// </summary>
        public InvalidResponseCauseException() {}

        /// <summary>
        /// Constructs a new instance of <see cref="InvalidResponseCauseException"/>.
        /// </summary>
        /// <param name="json">The JSON string containing the error.</param>
        /// <param name="status">The HTTP status code.</param>
        public InvalidResponseCauseException(string json, HttpStatusCode status = HttpStatusCode.BadRequest)
            : base(json, status)
        {
            try
            {
                var checkCause = ErrorJson.TryGetProperty(ErrorCauseKey, out var cause);
                ErrorCause = checkCause ? cause.GetString() : null;
            }
            catch
            {
                // ignored
            }
        }
    }
}
