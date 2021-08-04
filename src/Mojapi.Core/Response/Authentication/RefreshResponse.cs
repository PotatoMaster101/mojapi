using Mojapi.Core.Common;

namespace Mojapi.Core.Response.Authentication
{
    /// <summary>
    /// Represents a response from the refresh endpoint.
    /// </summary>
    public class RefreshResponse : BaseResponse
    {
        /// <summary>
        /// Gets the access and client tokens.
        /// </summary>
        /// <value>The access and client tokens.</value>
        public TokenPair Token { get; init; }

        /// <summary>
        /// Gets the selected profile.
        /// </summary>
        /// <value>The selected profile.</value>
        public Player SelectedProfile { get; init; }
    }
}
