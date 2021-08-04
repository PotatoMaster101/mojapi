using System;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Endpoint.Authenticated;

namespace UploadSkin
{
    /// <summary>
    /// Uploads a skin using <see cref="UploadSkinEndpoint"/>.
    /// </summary>
    internal static class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("Access Token:");
            var access = Console.ReadLine();
            Console.WriteLine("Skin Path:");
            var path = Console.ReadLine();
            Console.WriteLine("Slim? (true/false)");
            var slim = bool.Parse(Console.ReadLine() ?? "false");

            var upload = await new UploadSkinEndpoint(access, new Skin(path, slim ? Skin.SlimStyle : Skin.ClassicStyle)).Request();
            Console.WriteLine("============================================");
            Console.WriteLine($"New Skin URL:   {upload.ChangedSkin.Url}");
            Console.WriteLine($"Variant:        {upload.ChangedSkin.Variant}");
        }
    }
}
