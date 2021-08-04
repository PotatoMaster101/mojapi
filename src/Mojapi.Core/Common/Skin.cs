using System;
using System.Text.Json;

namespace Mojapi.Core.Common
{
    /// <summary>
    /// Stores the skin URL and variant info.
    /// </summary>
    public record Skin
    {
        /// <summary>
        /// The classic skin variant (Steve style).
        /// </summary>
        public const string ClassicStyle = "classic";

        /// <summary>
        /// The slim skin variant (Alex style).
        /// </summary>
        public const string SlimStyle = "slim";

        /// <summary>
        /// Gets the skin URL. Can also be a local file path.
        /// </summary>
        /// <value>The skin URL.</value>
        public string Url { get; }

        /// <summary>
        /// Gets the skin variant. If not specified, defaults to classic.
        /// </summary>
        /// <value>The skin variant.</value>
        public string Variant { get; }

        /// <summary>
        /// Gets whether the skin is classic style.
        /// </summary>
        /// <value>Whether the skin is classic style.</value>
        public bool IsClassic => Variant == ClassicStyle;

        /// <summary>
        /// Gets whether <see cref="Url"/> is a file path.
        /// </summary>
        /// <value>Whether <see cref="Url"/> is a file path.</value>
        public bool IsFile => !Url.StartsWith("http://") && !Url.StartsWith("https://");

        /// <summary>
        /// Constructs a new instance of <see cref="Skin"/>.
        /// </summary>
        /// <param name="url">The skin URL.</param>
        /// <param name="variant">The skin variant.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="url"/> or <paramref name="variant"/> is <see langword="null"/> or empty.</exception>
        public Skin(string url, string variant = ClassicStyle)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException("Invalid URL", nameof(url));
            if (string.IsNullOrEmpty(variant))
                throw new ArgumentException("Invalid variant", nameof(variant));

            Url = url;
            Variant = variant;
        }

        /// <summary>
        /// Constructs a new instance of <see cref="Skin"/>.
        /// </summary>
        /// <param name="json">The JSON containing the skin url and variant.</param>
        /// <param name="urlKey">The JSON key for skin URL.</param>
        /// <param name="variantKey">The JSON key for skin variant.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="json"/> does not contain a URL key.</exception>
        public Skin(JsonElement json, string urlKey = "url", string variantKey = "variant")
        {
            var checkUrl = json.TryGetProperty(urlKey, out var url);
            var checkVariant = json.TryGetProperty(variantKey, out var variant);
            if (!checkUrl)
                throw new ArgumentException("Invalid json", nameof(json));

            Url = url.GetString();
            Variant = checkVariant ? variant.GetString() : ClassicStyle;
        }

        /// <summary>
        /// Returns a JSON string representation of the current object.
        /// </summary>
        /// <param name="urlKey">The JSON key for skin URL.</param>
        /// <param name="variantKey">The JSON key for skin variant.</param>
        /// <returns>The JSON string representation of the current object.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="urlKey"/> or <paramref name="variantKey"/> is <see langword="null"/> or empty.</exception>
        public string ToJsonString(string urlKey = "url", string variantKey = "variant")
        {
            if (string.IsNullOrEmpty(urlKey))
                throw new ArgumentException("Invalid skin URL key", nameof(urlKey));
            if (string.IsNullOrEmpty(variantKey))
                throw new ArgumentException("Invalid skin variant key", nameof(variantKey));
            return $@"""{urlKey}"":""{Url}"",""{variantKey}"":""{Variant}""";
        }
    }
}
