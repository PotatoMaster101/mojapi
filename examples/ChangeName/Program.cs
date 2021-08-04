using System;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint.Authenticated;

namespace ChangeName
{
    /// <summary>
    /// Changes user name using <see cref="ChangeNameEndpoint"/>.
    /// </summary>
    internal static class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("Access Token:");
            var access = Console.ReadLine();
            Console.WriteLine("New Name:");
            var newName = Console.ReadLine();

            var name = await new ChangeNameEndpoint(access, newName).Request();
            Console.WriteLine("============================================");
            Console.WriteLine($"New Name:   {name.ChangedName}");
            Console.WriteLine($"UUID:       {name.Uuid}");
            Console.WriteLine($"Skin:       {name.Skin.Url}");
            Console.WriteLine($"Variant:    {name.Skin.Variant}");
        }
    }
}
