using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FISAssignment.Models
{
    public class BitcoinResponse
    {
        [JsonProperty("market_data")]
        public MarketData? MarketData { get; set; }

        [JsonProperty("links")]
        public Links? Links { get; set; }
    }

    public class MarketData
    {
        [JsonProperty("current_price")]
        public Dictionary<string, decimal>? CurrentPrice { get; set; }

        [JsonProperty("market_cap")]
        public Dictionary<string, decimal>? MarketCap { get; set; }

        [JsonProperty("total_volume")]
        public Dictionary<string, decimal>? TotalVolume { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public decimal PriceChangePercentage24h { get; set; }
    }

    public class Links
    {
        [JsonProperty("homepage")]
        public List<string>? Homepage { get; set; }
    }
}
