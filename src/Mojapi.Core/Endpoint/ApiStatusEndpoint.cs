using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint
{
    /// <summary>
    /// Represents the API status endpoint.
    /// </summary>
    public class ApiStatusEndpoint : BaseEndpoint<ApiStatusResponse>
    {
        /// <summary>
        /// The API status endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://status.mojang.com/check";

        /// <summary>
        /// Constructs a new instance of <see cref="ApiStatusEndpoint"/>.
        /// </summary>
        public ApiStatusEndpoint()
            : base(EndpointUrl) {}

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        public override async Task<ApiStatusResponse> Request()
        {
            var response = await RequestSender.SendGetRequest(this);
            using var json = JsonDocument.Parse(response.Data);
            var status = new ApiStatusResponse
            {
                Data = response.Data,
                Status = response.Status
            };

            var jsonArr = json.RootElement;
            for (var i = 0; i < jsonArr.GetArrayLength(); i++)
            {
                var info = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonArr[i].GetRawText())?.FirstOrDefault();
                if (info is not null)
                    status.SetStatus(info.Value.Key, info.Value.Value);
            }
            return status;
        }
    }
}
