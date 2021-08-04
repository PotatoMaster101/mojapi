using System;
using System.Text.Json;

namespace Mojapi.Core.Common
{
    /// <summary>
    /// Stores the access and client tokens.
    /// </summary>
    public record TokenPair
    {
        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <value>The access token.</value>
        public string AccessToken { get; }

        /// <summary>
        /// Gets the client token.
        /// </summary>
        /// <value>The client token.</value>
        public string ClientToken { get; }

        /// <summary>
        /// Gets whether the client token is optional.
        /// </summary>
        /// <value>Whether the client token is optional.</value>
        public bool ClientOptional { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="TokenPair"/>.
        /// </summary>
        /// <param name="access">The access token.</param>
        /// <param name="client">The client token. Can be null or empty if <paramref name="clientOptional"/> is <see langword="true"/>.</param>
        /// <param name="clientOptional">Whether the client token is optional, in which case ignore error checks.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="access"/> or <paramref name="client"/> is <see langword="null"/> or empty.</exception>
        public TokenPair(string access, string client, bool clientOptional = false)
        {
            if (string.IsNullOrEmpty(access))
                throw new ArgumentException("Invalid access token", nameof(access));
            if (!clientOptional && string.IsNullOrEmpty(client))
                throw new ArgumentException("Invalid client token", nameof(client));

            AccessToken = access;
            ClientToken = client;
            ClientOptional = clientOptional;
        }

        /// <summary>
        /// Constructs a new instance of <see cref="TokenPair"/>.
        /// </summary>
        /// <param name="json">The JSON containing the access token and client token.</param>
        /// <param name="accessKey">The JSON key for access token.</param>
        /// <param name="clientKey">The JSON key for client token.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="json"/> does not contain an access key.</exception>
        public TokenPair(JsonElement json, string accessKey = "accessToken", string clientKey = "clientToken")
        {
            var checkAccess = json.TryGetProperty(accessKey, out var access);
            var checkClient = json.TryGetProperty(clientKey, out var client);
            if (!checkAccess)
                throw new ArgumentException("Invalid json", nameof(json));

            AccessToken = access.GetString();
            if (!checkClient)
                ClientOptional = true;
            else
                ClientToken = client.GetString();
        }

        /// <summary>
        /// Returns a JSON string representation of the current object.
        /// </summary>
        /// <param name="accessKey">The JSON key for access token.</param>
        /// <param name="clientKey">The JSON key for client token.</param>
        /// <returns>The JSON string representation of the current object.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="accessKey"/> or <paramref name="clientKey"/> is <see langword="null"/> or empty.</exception>
        public string ToJsonString(string accessKey = "accessToken", string clientKey = "clientToken")
        {
            if (string.IsNullOrEmpty(accessKey))
                throw new ArgumentException("Invalid access token key", nameof(accessKey));
            if (string.IsNullOrEmpty(clientKey))
                throw new ArgumentException("Invalid client token key", nameof(clientKey));
            return ClientOptional ? $@"""{accessKey}"":""{AccessToken}""" : $@"""{accessKey}"":""{AccessToken}"",""{clientKey}"":""{ClientToken}""";
        }
    }
}
