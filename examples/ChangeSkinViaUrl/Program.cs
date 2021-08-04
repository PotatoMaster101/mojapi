using System;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Endpoint.Authenticated;

namespace ChangeSkinViaUrl
{
    /// <summary>
    /// Changes player skin from a URL using <see cref="ChangeSkinEndpoint"/>.
    /// </summary>
    internal static class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("Access Token:");
            var access = Console.ReadLine();
            Console.WriteLine("Skin URL:");
            var url = Console.ReadLine();
            Console.WriteLine("Slim? (true/false)");
            var slim = bool.Parse(Console.ReadLine() ?? "false");

            var skin = new Skin(url, slim ? Skin.SlimStyle : Skin.ClassicStyle);
            var status = await new ChangeSkinEndpoint(access, skin).Request();
            Console.WriteLine("============================================");
            Console.WriteLine($"New Skin URL:   {status.ChangedSkin.Url}");
            Console.WriteLine($"Variant:        {status.ChangedSkin.Variant}");
        }
    }
}
