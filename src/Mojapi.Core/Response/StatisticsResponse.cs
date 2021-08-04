namespace Mojapi.Core.Response
{
    /// <summary>
    /// Represents a response from the statistics endpoint.
    /// </summary>
    public class StatisticsResponse : BaseResponse
    {
        /// <summary>
        /// Gets the total statistics.
        /// </summary>
        /// <value>The total statistics.</value>
        public long Total { get; init; }

        /// <summary>
        /// Gets the statistics in the last 24h.
        /// </summary>
        /// <value>The statistics in the last 24h.</value>
        public long Last24H { get; init; }

        /// <summary>
        /// Gets the sales velocity per second.
        /// </summary>
        /// <value>The sales velocity per second.</value>
        public double SalesVelocity { get; init; }
    }
}
