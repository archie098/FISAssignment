using FISAssignment.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISAssignment.Tests
{
    [Parallelizable(ParallelScope.All)]
    public class BitCoinAPITest : TestFixture
    {
        protected override bool CaptureScreenshot => false;
        [Test]
        public async Task ValidateBitcoinApi()
        {
            Test.Info("Fetching Bitcoin data from API");
            var bitcoin = await apiClient.GetBitcoinDataAsync();
            Test.Info("Validating market data");
            var marketData = bitcoin.MarketData;

            Assert.That(marketData.CurrentPrice.ContainsKey("usd"), Is.True, "USD price tracking key is missing.");
            Assert.That(marketData.CurrentPrice.ContainsKey("gbp"), Is.True, "GBP price tracking key is missing.");
            Assert.That(marketData.CurrentPrice.ContainsKey("eur"), Is.True, "EUR price tracking key is missing.");

            Assert.That(marketData.CurrentPrice["usd"], Is.GreaterThan(0), "USD price must be a positive number.");
            Assert.That(marketData.CurrentPrice["gbp"], Is.GreaterThan(0), "GBP price must be a positive number.");
            Assert.That(marketData.CurrentPrice["eur"], Is.GreaterThan(0), "EUR price must be a positive number.");

            Test.Info("Validating Market Cap and Volume metrics");
            Assert.That(marketData.MarketCap["usd"], Is.GreaterThan(0), "Market Cap metric should be greater than 0.");
            Assert.That(marketData.TotalVolume["usd"], Is.GreaterThan(0), "Total Volume metric should be greater than 0.");
            Assert.That(marketData.PriceChangePercentage24h, Is.Not.EqualTo(0), "24-hour price change percentage data is missing.");
            Test.Info("Validating homepage links");
            Assert.That(bitcoin.Links.Homepage, Is.Not.Null.And.Not.Empty, "The homepage link array should not be empty.");
        }

    }
}
