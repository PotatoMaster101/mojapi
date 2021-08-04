using System;

namespace Mojapi.Core.Response
{
    /// <summary>
    /// Represents a response from the API status endpoint.
    /// </summary>
    public class ApiStatusResponse : BaseResponse
    {
        /// <summary>
        /// Minecraft service URL.
        /// </summary>
        public const string MinecraftService = "minecraft.net";

        /// <summary>
        /// Session service URL.
        /// </summary>
        public const string SessionService = "session.minecraft.net";

        /// <summary>
        /// Account service URL.
        /// </summary>
        public const string AccountService = "account.mojang.com";

        /// <summary>
        /// Auth server service URL.
        /// </summary>
        public const string AuthServerService = "authserver.mojang.com";

        /// <summary>
        /// Session server service URL.
        /// </summary>
        public const string SessionServerService = "sessionserver.mojang.com";

        /// <summary>
        /// API service URL.
        /// </summary>
        public const string ApiService = "api.mojang.com";

        /// <summary>
        /// Texture service URL.
        /// </summary>
        public const string TextureService = "textures.minecraft.net";

        /// <summary>
        /// Mojang service URL.
        /// </summary>
        public const string MojangService = "mojang.com";

        /// <summary>
        /// Gets the status for minecraft.net.
        /// </summary>
        /// <value>The status for minecraft.net.</value>
        public ApiStatus Minecraft { get; private set; }

        /// <summary>
        /// Gets the status for session.minecraft.net.
        /// </summary>
        /// <value>The status for session.minecraft.net.</value>
        public ApiStatus Session { get; private set; }

        /// <summary>
        /// Gets the status for account.mojang.com.
        /// </summary>
        /// <value>The status for account.mojang.com.</value>
        public ApiStatus Account { get; private set; }

        /// <summary>
        /// Gets the status for authserver.mojang.com.
        /// </summary>
        /// <value>The status for authserver.mojang.com.</value>
        public ApiStatus AuthServer { get; private set; }

        /// <summary>
        /// Gets the status for sessionserver.mojang.com.
        /// </summary>
        /// <value>The status for sessionserver.mojang.com.</value>
        public ApiStatus SessionServer { get; private set; }

        /// <summary>
        /// Gets the status for api.mojang.com.
        /// </summary>
        /// <value>The status for api.mojang.com.</value>
        public ApiStatus Api { get; private set; }

        /// <summary>
        /// Gets the status for textures.minecraft.net.
        /// </summary>
        /// <value>The status for textures.minecraft.net.</value>
        public ApiStatus Texture { get; private set; }

        /// <summary>
        /// Gets the status for mojang.com.
        /// </summary>
        /// <value>The status for mojang.com.</value>
        public ApiStatus Mojang { get; private set; }

        /// <summary>
        /// Sets the status of the specified API.
        /// </summary>
        /// <param name="api">The API to set status.</param>
        /// <param name="status">The status string of the API.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="api"/> is invalid.</exception>
        public void SetStatus(string api, string status)
        {
            SetStatus(api, ParseStatus(status));
        }

        /// <summary>
        /// Sets the status of the specified API.
        /// </summary>
        /// <param name="api">The API to set status.</param>
        /// <param name="status">The status of the API.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="api"/> is invalid.</exception>
        public void SetStatus(string api, ApiStatus status)
        {
            switch (api)
            {
                case MinecraftService:
                    Minecraft = status;
                    return;
                case SessionService:
                    Session = status;
                    return;
                case AccountService:
                    Account = status;
                    return;
                case AuthServerService:
                    AuthServer = status;
                    return;
                case SessionServerService:
                    SessionServer = status;
                    return;
                case ApiService:
                    Api = status;
                    return;
                case TextureService:
                    Texture = status;
                    return;
                case MojangService:
                    Mojang = status;
                    return;
            }
            throw new ArgumentException("Invalid API", nameof(api));
        }

        /// <summary>
        /// Converts the given status string into <see cref="ApiStatus"/>.
        /// </summary>
        /// <param name="status">The status string to parse.</param>
        /// <returns>The parsed status.</returns>
        public static ApiStatus ParseStatus(string status)
        {
            return status switch
            {
                "green" => ApiStatus.Green,
                "yellow" => ApiStatus.Yellow,
                "red" => ApiStatus.Red,
                _ => ApiStatus.Unknown
            };
        }

        /// <summary>
        /// Possible API status.
        /// </summary>
        public enum ApiStatus
        {
            /// <summary>
            /// Endpoint status unknown.
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// Endpoint is functional.
            /// </summary>
            Green,

            /// <summary>
            /// Endpoint is partially functional.
            /// </summary>
            Yellow,

            /// <summary>
            /// Endpoint is down.
            /// </summary>
            Red
        }
    }
}
