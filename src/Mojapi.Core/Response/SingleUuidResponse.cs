using Mojapi.Core.Common;

namespace Mojapi.Core.Response
{
    /// <summary>
    /// Represents a response from the username to UUID endpoint.
    /// </summary>
    public class SingleUuidResponse : BaseResponse
    {
        /// <summary>
        /// Gets the player information.
        /// </summary>
        /// <value>The player information.</value>
        public PlayerInfo Player { get; init; }
    }
}
