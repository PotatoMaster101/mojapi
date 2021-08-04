using System.Threading.Tasks;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint
{
    /// <summary>
    /// Represents the blocked servers endpoint.
    /// </summary>
    public class BlockedServerEndpoint : BaseEndpoint<BlockedServerResponse>
    {
        /// <summary>
        /// The blocked servers endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://sessionserver.mojang.com/blockedservers";

        /// <summary>
        /// Constructs a new instance of <see cref="BlockedServerEndpoint"/>.
        /// </summary>
        public BlockedServerEndpoint()
            : base(EndpointUrl) {}

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        public override async Task<BlockedServerResponse> Request()
        {
            var response = await RequestSender.SendGetRequest(this);
            return new BlockedServerResponse
            {
                Data = response.Data,
                Status = response.Status,
                Hashes = response.Data.Split("\n")
            };
        }
    }
}
