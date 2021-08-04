using System.Collections.Generic;
using Mojapi.Core.Common;

namespace Mojapi.Core.Response.Authentication
{
    /// <summary>
    /// Represents a response from the authentication endpoint.
    /// </summary>
    public class AuthenticationResponse : BaseResponse
    {
        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value>The player.</value>
        public Player Player { get; init; }

        /// <summary>
        /// Gets the access and client tokens.
        /// </summary>
        /// <value>The access and client tokens.</value>
        public TokenPair Token { get; init; }

        /// <summary>
        /// Gets the list of available profiles.
        /// </summary>
        /// <value>The list of available profiles.</value>
        public IEnumerable<Player> AvailableProfiles { get; init; }

        /// <summary>
        /// Gets the selected profile.
        /// </summary>
        /// <value>The selected profile.</value>
        public Player SelectedProfile { get; init; }
    }
}
