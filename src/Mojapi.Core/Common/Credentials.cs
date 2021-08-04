using System;

namespace Mojapi.Core.Common
{
    /// <summary>
    /// Stores the user credentials.
    /// </summary>
    public record Credentials
    {
        /// <summary>
        /// Gets the player username. Can be email address too.
        /// </summary>
        /// <value>The player username.</value>
        public string Username { get; }

        /// <summary>
        /// Gets the player password.
        /// </summary>
        /// <value>The player password.</value>
        public string Password { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="Credentials"/>.
        /// </summary>
        /// <param name="username">The player username.</param>
        /// <param name="password">The player password.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> or <paramref name="password"/> is <see langword="null"/> or empty.</exception>
        public Credentials(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Invalid username", nameof(username));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Invalid password", nameof(password));

            Username = username;
            Password = password;
        }

        /// <summary>
        /// Returns a JSON representation of the current object.
        /// </summary>
        /// <param name="userKey">The JSON key for username.</param>
        /// <param name="passKey">The JSON key for password.</param>
        /// <returns>The JSON string representation of the current object.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="userKey"/> or <paramref name="passKey"/> is <see langword="null"/> or empty.</exception>
        public string ToJsonString(string userKey = "username", string passKey = "password")
        {
            if (string.IsNullOrEmpty(userKey))
                throw new ArgumentException("Invalid username key", nameof(userKey));
            if (string.IsNullOrEmpty(passKey))
                throw new ArgumentException("Invalid password key", nameof(passKey));
            return $@"""{userKey}"":""{Username}"",""{passKey}"":""{Password}""";
        }
    }
}
