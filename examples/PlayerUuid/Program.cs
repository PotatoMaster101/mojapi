using System;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint;

namespace PlayerUuid
{
    /// <summary>
    /// Retrieves player UUIDs using <see cref="MultipleUuidEndpoint"/>.
    /// </summary>
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("No username", nameof(args));

            int i;
            for (i = 10; i < args.Length; i += 10)
            {
                // this endpoint can only take max 10 usernames at once
                var response = await new MultipleUuidEndpoint(args[(i - 10)..i]).Request();
                foreach (var player in response.Players)
                    Console.WriteLine($"{player.Username} : {player.Uuid}");
            }

            // get remaining
            var remaining = await new MultipleUuidEndpoint(args[(i - 10)..]).Request();
            foreach (var player in remaining.Players)
                Console.WriteLine($"{player.Username} : {player.Uuid}");
        }
    }
}
