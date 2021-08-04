using System;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint;

namespace ServiceStatus
{
    /// <summary>
    /// Retrieves API status using <see cref="ApiStatusEndpoint"/>.
    /// </summary>
    internal static class Program
    {
        private static async Task Main()
        {
            var response = await new ApiStatusEndpoint().Request();
            Console.WriteLine($"minecraft.net:              {response.Minecraft.ToString()}");
            Console.WriteLine($"session.minecraft.net:      {response.Session.ToString()}");
            Console.WriteLine($"account.mojang.com:         {response.Account.ToString()}");
            Console.WriteLine($"authserver.mojang.com:      {response.AuthServer.ToString()}");
            Console.WriteLine($"sessionserver.mojang.com:   {response.SessionServer.ToString()}");
            Console.WriteLine($"api.mojang.com:             {response.Api.ToString()}");
            Console.WriteLine($"textures.minecraft.net:     {response.Texture.ToString()}");
            Console.WriteLine($"mojang.com:                 {response.Mojang.ToString()}");
        }
    }
}
