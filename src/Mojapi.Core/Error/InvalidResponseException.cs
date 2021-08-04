using System;
using System.Net;
using System.Text.Json;

namespace Mojapi.Core.Error
{
    /// <summary>
    /// Represents an error with the API response which contains an error name and message.
    /// </summary>
    public class InvalidResponseException : InvalidOperationException
    {
        /// <summary>
        /// The key for retrieving the error name.
        /// </summary>
        private const string ErrorNameKey = "error";

        /// <summary>
        /// The key for retrieving the error message.
        /// </summary>
        private const string ErrorMessageKey = "errorMessage";

        /// <summary>
        /// Gets the error name.
        /// </summary>
        /// <value>The error name.</value>
        public string ErrorName { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; }

        /// <summary>
        /// Gets the error HTTP status code.
        /// </summary>
        /// <value>The error status code.</value>
        public HttpStatusCode Status { get; }

        /// <summary>
        /// Gets the parsed error JSON.
        /// </summary>
        /// <value>The parsed error JSON.</value>
        protected JsonElement ErrorJson { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="InvalidResponseException"/>.
        /// </summary>
        public InvalidResponseException() :
            base("Bad response")
        {
            Status = HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// Constructs a new instance of <see cref="InvalidResponseException"/>.
        /// </summary>
        /// <param name="json">The JSON string containing the error.</param>
        /// <param name="status">The HTTP status code.</param>
        public InvalidResponseException(string json, HttpStatusCode status = HttpStatusCode.BadRequest)
            : base("Bad response")
        {
            Status = status;
            try
            {
                using var jsonDoc = JsonDocument.Parse(json);
                ErrorJson = jsonDoc.RootElement.Clone();

                var checkErrName = ErrorJson.TryGetProperty(ErrorNameKey, out var errName);
                var checkErrMsg = ErrorJson.TryGetProperty(ErrorMessageKey, out var errMsg);
                ErrorName = checkErrName ? errName.GetString() : null;
                ErrorMessage = checkErrMsg ? errMsg.GetString() : null;
            }
            catch
            {
                // ignored
            }
        }
    }
}
