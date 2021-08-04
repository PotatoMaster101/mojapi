using System;
using System.Text.Json;

namespace Mojapi.Core.Common
{
    /// <summary>
    /// Stores the player username and UUID.
    /// </summary>
    public record Player
    {
        /// <summary>
        /// Gets the player username.
        /// </summary>
        /// <value>The player username.</value>
        public string Username { get; }

        /// <summary>
        /// Gets the player UUID.
        /// </summary>
        /// <value>The player UUID.</value>
        public string Uuid { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="Player"/>.
        /// </summary>
        /// <param name="json">The JSON containing the username and UUID.</param>
        /// <param name="userKey">The JSON key for username.</param>
        /// <param name="uuidKey">The JSON key for UUID.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="json"/> does not contain one of the key.</exception>
        public Player(JsonElement json, string userKey = "name", string uuidKey = "id")
        {
            var checkName = json.TryGetProperty(userKey, out var name);
            var checkId = json.TryGetProperty(uuidKey, out var id);
            if (!checkName || !checkId)
                throw new ArgumentException("Invalid json", nameof(json));

            Username = name.GetString();
            Uuid = id.GetString();
        }
    }
}
