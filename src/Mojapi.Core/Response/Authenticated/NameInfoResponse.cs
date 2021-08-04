namespace Mojapi.Core.Response.Authenticated
{
    /// <summary>
    /// Represents a response from the name information endpoint.
    /// </summary>
    public class NameInfoResponse : BaseResponse
    {
        /// <summary>
        /// Gets the name change date string. If this is <see langword="null"/>, the name is original.
        /// </summary>
        /// <value>The name change date string.</value>
        public string ChangedAt { get; init; }

        /// <summary>
        /// Gets the name creation date string. This is typically the date of account creation.
        /// </summary>
        /// <value>The name creation date string.</value>
        public string CreatedAt { get; init; }

        /// <summary>
        /// Gets whether name change is allowed.
        /// </summary>
        /// <value>Whether name change is allowed.</value>
        public bool NameChangeAllowed { get; init; }
    }
}
