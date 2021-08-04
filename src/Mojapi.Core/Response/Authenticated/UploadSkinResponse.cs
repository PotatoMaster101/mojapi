using Mojapi.Core.Common;

namespace Mojapi.Core.Response.Authenticated
{
    /// <summary>
    /// Represents a response from the upload skin endpoint.
    /// </summary>
    public class UploadSkinResponse : BaseResponse
    {
        /// <summary>
        /// Gets the uploaded skin.
        /// </summary>
        /// <value>The uploaded skin.</value>
        public Skin ChangedSkin { get; init; }
    }
}
