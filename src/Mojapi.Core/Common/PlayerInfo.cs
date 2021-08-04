using System;
using System.Text.Json;

namespace Mojapi.Core.Common
{
    /// <summary>
    /// Stores the player info such as legacy or demo account.
    /// </summary>
    public record PlayerInfo : Player
    {
        /// <summary>
        /// Gets whether the player account is legacy.
        /// </summary>
        /// <value>Whether the player account is legacy.</value>
        public bool Legacy { get; }

        /// <summary>
        /// Gets whether the player account is unpaid.
        /// </summary>
        /// <value>Gets whether the player account is unpaid.</value>
        public bool Demo { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="PlayerInfo"/>.
        /// </summary>
        /// <param name="json">The JSON containing the username and UUID.</param>
        /// <param name="userKey">The JSON key for username.</param>
        /// <param name="uuidKey">The JSON key for UUID.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="json"/> does not contain one of the key.</exception>
        public PlayerInfo(JsonElement json, string userKey = "name", string uuidKey = "id")
            : base(json, userKey, uuidKey)
        {
            Legacy = json.TryGetProperty("legacy", out _);
            Demo = json.TryGetProperty("demo", out _);
        }
    }
}
