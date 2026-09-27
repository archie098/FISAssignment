using FISAssignment.ApiHelper;
using FISAssignment.Models;
using Microsoft.Playwright;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISAssignment.APIClient
{
    public class BitCoinClient : APIBase
    {

        public BitCoinClient(IAPIRequestContext request) : base(request) { }

        public async Task<BitcoinResponse> GetBitcoinDataAsync()
        {
            var response = await _request.GetAsync($"{config.baseURL}/coins/bitcoin?localization=false&tickers=false&community_data=false&developer_data=false&sparkline=false");

            if (!response.Ok)
            {
                throw new HttpRequestException($"API request failed with status {response.Status}: {response.StatusText}");
            }

            var jsonString = await response.TextAsync();
            var data = JsonConvert.DeserializeObject<BitcoinResponse>(jsonString);

            return data ?? throw new InvalidOperationException("Failed to deserialize Bitcoin API response.");
        }


    }
}
