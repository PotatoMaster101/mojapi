using Mojapi.Core.Common;

namespace Mojapi.Core.Response.Authenticated
{
    /// <summary>
    /// Represents a response from the change skin endpoint.
    /// </summary>
    public class ChangeSkinResponse : BaseResponse
    {
        /// <summary>
        /// Gets the changed skin.
        /// </summary>
        /// <value>The changed skin.</value>
        public Skin ChangedSkin { get; init; }
    }
}
