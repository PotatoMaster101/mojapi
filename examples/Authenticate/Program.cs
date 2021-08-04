using System;
using System.Threading.Tasks;
using Mojapi.Core.Common;
using Mojapi.Core.Endpoint.Authentication;

namespace Authenticate
{
    /// <summary>
    /// Authenticates username and password using <see cref="AuthenticationEndpoint"/>.
    /// </summary>
    internal static class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("Email:");
            var username = Console.ReadLine();
            Console.WriteLine("Password:");
            var password = Console.ReadLine();

            var auth = await new AuthenticationEndpoint(new Credentials(username, password), true).Request();
            Console.WriteLine($"Client token: {auth.Token.ClientToken}");
            Console.WriteLine($"Access token: {auth.Token.AccessToken}");
            Console.WriteLine($"Username:     {auth.SelectedProfile?.Username}");
            Console.WriteLine($"UUID:         {auth.SelectedProfile?.Uuid}");
        }
    }
}
