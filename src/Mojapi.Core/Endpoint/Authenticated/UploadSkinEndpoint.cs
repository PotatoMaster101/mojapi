using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Error;
using Mojapi.Core.Response.Authenticated;

namespace Mojapi.Core.Endpoint.Authenticated
{
    /// <summary>
    /// Represents the upload skin endpoint.
    /// </summary>
    public class UploadSkinEndpoint : BaseAuthenticatedEndpoint<UploadSkinResponse>
    {
        /// <summary>
        /// The upload skin endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.minecraftservices.com/minecraft/profile/skins";

        /// <summary>
        /// The skin to upload.
        /// </summary>
        private readonly Skin _skin;

        /// <summary>
        /// Constructs a new instance of <see cref="UploadSkinEndpoint"/>.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="skin">The skin to upload.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="accessToken"/> is <see langword="null"/> or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="skin"/> is <see langword="null"/>.</exception>
        public UploadSkinEndpoint(string accessToken, Skin skin)
            : base(EndpointUrl, accessToken)
        {
            ContentType = "multipart/form-data";
            _skin = skin ?? throw new ArgumentNullException(nameof(skin));
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        /// <exception cref="InvalidResponseException">Thrown when the request failed.</exception>
        public override async Task<UploadSkinResponse> Request()
        {
            using var formData = GetFormPostData();
            var response = await RequestSender.SendFormPostRequest(this, formData);
            if (response.Status != HttpStatusCode.OK)
                throw new InvalidResponseException(response.Data, response.Status);

            using var json = JsonDocument.Parse(response.Data);
            return new UploadSkinResponse
            {
                Data = response.Data,
                Status = response.Status,
                ChangedSkin = new Skin(json.RootElement.GetProperty("skins")[0])
            };
        }

        /// <summary>
        /// Returns the form post data using the specified skin.
        /// </summary>
        /// <returns>The <see cref="HttpContent"/> containing the skin image.</returns>
        private HttpContent GetFormPostData()
        {
            var variant = new StringContent(_skin.Variant);
            variant.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "variant"
            };

            var file = new ByteArrayContent(File.ReadAllBytes(_skin.Url));
            file.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = _skin.Url
            };

            return new MultipartFormDataContent
            {
                variant,
                file
            };
        }
    }
}
