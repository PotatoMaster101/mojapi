using Mojapi.Core.Common;

namespace Mojapi.Core.Response.Authenticated
{
    /// <summary>
    /// Represents a response from the player profile endpoint.
    /// </summary>
    public class ProfileResponse : BaseResponse
    {
        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value>The player.</value>
        public Player Player { get; init; }

        /// <summary>
        /// Gets the player skin.
        /// </summary>
        /// <value>The player skin.</value>
        public Skin Skin { get; init; }
    }
}
