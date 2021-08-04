namespace Mojapi.Core.Response.Authenticated
{
    /// <summary>
    /// Represents a response from the name availability endpoint.
    /// </summary>
    public class NameAvailabilityResponse : BaseResponse
    {
        /// <summary>
        /// Gets the name availability status.
        /// </summary>
        /// <value>The name availability status.</value>
        public string NameStatus { get; init; }
    }
}
