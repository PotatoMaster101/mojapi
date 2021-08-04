using System.Collections.Generic;

namespace Mojapi.Core.Response
{
    /// <summary>
    /// Represents a response from the blocked servers endpoint.
    /// </summary>
    public class BlockedServerResponse : BaseResponse
    {
        /// <summary>
        /// Gets the list of blocked servers' SHA1 hashes.
        /// </summary>
        public IEnumerable<string> Hashes { get; init; }
    }
}
