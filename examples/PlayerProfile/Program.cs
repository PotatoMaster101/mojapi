using System;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint;

namespace PlayerProfile
{
    /// <summary>
    /// Retrieves player profile using <see cref="ProfileEndpoint"/>.
    /// </summary>
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("No username", nameof(args));

            var player = await new SingleUuidEndpoint(args[0]).Request();
            var profile = await new ProfileEndpoint(player.Player.Uuid).Request();
            Console.WriteLine($"Username:   {profile.Player.Username}");
            Console.WriteLine($"UUID:       {profile.Player.Uuid}");
            Console.WriteLine($"Legacy:     {profile.Player.Legacy}");
            Console.WriteLine($"Skin URL:   {profile.Texture.SkinUrl ?? "None"}");
            Console.WriteLine($"Slim skin:  {profile.Texture.SlimSkin}");
            Console.WriteLine($"Cape URL:   {profile.Texture.CapeUrl ?? "None"}");
        }
    }
}
