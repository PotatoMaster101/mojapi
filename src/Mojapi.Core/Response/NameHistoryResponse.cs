using System.Collections.Generic;

namespace Mojapi.Core.Response
{
    /// <summary>
    /// Represents a response from the usernames to UUIDs endpoint.
    /// </summary>
    public class NameHistoryResponse : BaseResponse
    {
        /// <summary>
        /// Gets the list of name history from the response.
        /// </summary>
        /// <value>The list of name history from the response.</value>
        public IList<NameHistory> History { get; } = new List<NameHistory>();

        /// <summary>
        /// Represents a name history in the response.
        /// </summary>
        /// <param name="Name">The name of the history entry.</param>
        /// <param name="Timestamp">The timestamp of the history entry.</param>
        public record NameHistory(string Name, long Timestamp);
    }
}
