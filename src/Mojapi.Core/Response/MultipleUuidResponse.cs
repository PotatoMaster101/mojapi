using System.Collections.Generic;
using Mojapi.Core.Common;

namespace Mojapi.Core.Response
{
    /// <summary>
    /// Represents a response from the usernames to UUIDs endpoint.
    /// </summary>
    public class MultipleUuidResponse : BaseResponse
    {
        /// <summary>
        /// Gets the list of players from the response.
        /// </summary>
        /// <value>The list of players from the response.</value>
        public IList<PlayerInfo> Players { get; } = new List<PlayerInfo>();
    }
}
