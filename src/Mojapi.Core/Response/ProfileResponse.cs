using System;
using System.Text;
using System.Text.Json;
using Mojapi.Core.Common;

namespace Mojapi.Core.Response
{
    /// <summary>
    /// Represents a response from the player profile endpoint.
    /// </summary>
    public class ProfileResponse : BaseResponse
    {
        /// <summary>
        /// Gets the player information.
        /// </summary>
        /// <value>The player information.</value>
        public PlayerInfo Player { get; init; }

        /// <summary>
        /// Gets the player texture information.
        /// </summary>
        /// <value>The player texture information.</value>
        public TextureProperty Texture { get; init; }

        /// <summary>
        /// Represents the player texture properties.
        /// </summary>
        public record TextureProperty : Property
        {
            /// <summary>
            /// Gets the skin URL.
            /// </summary>
            /// <value>The skin URL.</value>
            public string SkinUrl { get; }

            /// <summary>
            /// Gets the cape URL.
            /// </summary>
            /// <value>The cape URL.</value>
            public string CapeUrl { get; }

            /// <summary>
            /// Gets whether the skin is slim (Alex style).
            /// </summary>
            /// <value>Whether the skin is slim.</value>
            public bool SlimSkin { get; }

            /// <summary>
            /// Constructs a new instance of <see cref="TextureProperty"/>.
            /// </summary>
            /// <param name="json">The JSON data containing the texture information.</param>
            public TextureProperty(JsonElement json)
                : base(json)
            {
                var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(Value));
                using var jsonDoc = JsonDocument.Parse(decoded);
                var texture = jsonDoc.RootElement.GetProperty("textures");
                if (texture.TryGetProperty("SKIN", out var skin))
                {
                    SkinUrl = skin.GetProperty("url").GetString();
                    SlimSkin = skin.TryGetProperty("metadata", out _);
                }
                if (texture.TryGetProperty("CAPE", out var cape))
                    CapeUrl = cape.GetProperty("url").GetString();
            }
        }
    }
}
