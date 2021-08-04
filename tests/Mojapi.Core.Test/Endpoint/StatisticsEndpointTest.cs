using System;
using System.Net;
using System.Threading.Tasks;
using Mojapi.Core.Endpoint;
using Xunit;

namespace Mojapi.Core.Test.Endpoint
{
    /// <summary>
    /// Unit test for <see cref="StatisticsEndpoint"/>.
    /// </summary>
    public class StatisticsEndpointTest
    {
        [Theory]
        [InlineData(@"{""metricKeys"":[""item_sold_minecraft"",""prepaid_card_redeemed_minecraft""]}", StatisticsMetric.ItemSoldMinecraft, StatisticsMetric.PrepaidCardRedeemedMinecraft)]
        [InlineData(@"{""metricKeys"":[""""]}", (StatisticsMetric) 99999)]
        public void Constructor_Sets_Members(string post, params StatisticsMetric[] metrics)
        {
            // arrange, act
            var endpoint = new StatisticsEndpoint(metrics);

            // assert
            Assert.Equal(post, endpoint.PostData);
            Assert.Equal(metrics, endpoint.Metrics);
        }

        [Fact]
        public void Constructor_Throws_OnNullParams()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentNullException>(() => new StatisticsEndpoint(null));
        }

        [Fact]
        public void Constructor_Throws_OnEmpty()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new StatisticsEndpoint(Array.Empty<StatisticsMetric>()));
        }

        [Theory]
        [InlineData(StatisticsMetric.ItemSoldMinecraft)]
        [InlineData(StatisticsMetric.PrepaidCardRedeemedMinecraft)]
        [InlineData(StatisticsMetric.ItemSoldCobalt)]
        [InlineData(StatisticsMetric.ItemSoldScrolls)]
        [InlineData(StatisticsMetric.PrepaidCardRedeemedCobalt)]
        [InlineData(StatisticsMetric.ItemSoldDungeons)]
        [InlineData(StatisticsMetric.ItemSoldMinecraft, StatisticsMetric.ItemSoldDungeons, StatisticsMetric.PrepaidCardRedeemedMinecraft)]
        public async Task Request_Returns_CorrectResponse(params StatisticsMetric[] metrics)
        {
            // arrange
            var endpoint = new StatisticsEndpoint(metrics);

            // act
            var response = await endpoint.Request();

            // assert
            Assert.Equal(HttpStatusCode.OK, response.Status);
            Assert.True(response.Data.Length > 0);
            Assert.True(response.Total >= 0);
            Assert.True(response.Last24H >= 0);
            Assert.True(response.SalesVelocity >= 0.0);
        }
    }
}
