using System;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint;

namespace PlayerNameHistory
{
    /// <summary>
    /// Retrieves player name history using <see cref="NameHistoryEndpoint"/>.
    /// </summary>
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("No username", nameof(args));

            var player = await new SingleUuidEndpoint(args[0]).Request();
            var history = await new NameHistoryEndpoint(player.Player.Uuid).Request();
            foreach (var (name, timestamp) in history.History)
                Console.WriteLine($"{name} : {(timestamp <= 0 ? "Original" : FromUnixEpoch(timestamp))}");
        }

        private static DateTime FromUnixEpoch(long milliseconds)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;
        }
    }
}
