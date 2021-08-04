using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Mojapi.Core.Response;

namespace Mojapi.Core.Endpoint
{
    /// <summary>
    /// All possible statistics metrics.
    /// </summary>
    public enum StatisticsMetric
    {
        /// <summary>
        /// The item_sold_minecraft metric.
        /// </summary>
        ItemSoldMinecraft = 0,

        /// <summary>
        /// The prepaid_card_redeemed_minecraft metric.
        /// </summary>
        PrepaidCardRedeemedMinecraft,

        /// <summary>
        /// The item_sold_cobalt metric.
        /// </summary>
        ItemSoldCobalt,

        /// <summary>
        /// The item_sold_scrolls metric.
        /// </summary>
        ItemSoldScrolls,

        /// <summary>
        /// The prepaid_card_redeemed_cobalt metric.
        /// </summary>
        PrepaidCardRedeemedCobalt,

        /// <summary>
        /// The item_sold_dungeons metric.
        /// </summary>
        ItemSoldDungeons
    }

    /// <summary>
    /// Represents the statistics endpoint.
    /// </summary>
    public class StatisticsEndpoint : BaseEndpoint<StatisticsResponse>
    {
        /// <summary>
        /// The statistics endpoint URL.
        /// </summary>
        private const string EndpointUrl = "https://api.mojang.com/orders/statistics";

        /// <summary>
        /// Gets the metrics to query.
        /// </summary>
        /// <value>The metrics to query.</value>
        public IEnumerable<StatisticsMetric> Metrics { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="StatisticsEndpoint"/>.
        /// </summary>
        /// <param name="metrics">The metrics to query.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="metrics"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="metrics"/> is empty.</exception>
        public StatisticsEndpoint(IEnumerable<StatisticsMetric> metrics)
            : base(EndpointUrl)
        {
            if (metrics is null)
                throw new ArgumentNullException(nameof(metrics));

            var metricList = metrics.ToList();
            if (!metricList.Any())
                throw new ArgumentException("Invalid metrics", nameof(metrics));

            Metrics = metricList;
            PostData = $@"{{""metricKeys"":[""{string.Join("\",\"", metricList.Select(GetMetricString))}""]}}";
        }

        /// <summary>
        /// Sends a request to the endpoint and returns the response.
        /// </summary>
        /// <returns>The response from the endpoint.</returns>
        public override async Task<StatisticsResponse> Request()
        {
            var response = await RequestSender.SendPostRequest(this);
            using var json = JsonDocument.Parse(response.Data);
            var root = json.RootElement;
            return new StatisticsResponse
            {
                Data = response.Data,
                Status = response.Status,
                Total = root.GetProperty("total").GetInt64(),
                Last24H = root.GetProperty("last24h").GetInt64(),
                SalesVelocity = root.GetProperty("saleVelocityPerSeconds").GetDouble()
            };
        }

        /// <summary>
        /// Returns the string representation of the given metric.
        /// </summary>
        /// <param name="metric">The metric to convert.</param>
        /// <returns>The string representation of the given metric.</returns>
        private static string GetMetricString(StatisticsMetric metric)
        {
            return metric switch
            {
                StatisticsMetric.ItemSoldMinecraft => "item_sold_minecraft",
                StatisticsMetric.PrepaidCardRedeemedMinecraft => "prepaid_card_redeemed_minecraft",
                StatisticsMetric.ItemSoldCobalt => "item_sold_cobalt",
                StatisticsMetric.ItemSoldScrolls => "item_sold_scrolls",
                StatisticsMetric.PrepaidCardRedeemedCobalt => "prepaid_card_redeemed_cobalt",
                StatisticsMetric.ItemSoldDungeons => "item_sold_dungeons",
                _ => string.Empty
            };
        }
    }
}
