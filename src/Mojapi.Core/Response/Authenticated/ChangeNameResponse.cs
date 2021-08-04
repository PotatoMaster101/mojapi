using Mojapi.Core.Common;

namespace Mojapi.Core.Response.Authenticated
{
    /// <summary>
    /// Represents a response from the change name endpoint.
    /// </summary>
    public class ChangeNameResponse : BaseResponse
    {
        /// <summary>
        /// Gets the newly changed username.
        /// </summary>
        /// <value>The newly changed username.</value>
        public string ChangedName { get; init; }

        /// <summary>
        /// Gets the player UUID.
        /// </summary>
        /// <value>The player UUID.</value>
        public string Uuid { get; init; }

        /// <summary>
        /// Gets the player skin.
        /// </summary>
        /// <value>The player skin.</value>
        public Skin Skin { get; init; }
    }
}
