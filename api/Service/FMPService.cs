﻿
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;

using Newtonsoft.Json;

namespace api.Service
{
    public class FMPService : IFMPService
    {
        private HttpClient _httpClient;
        private IConfiguration _config;

        public FMPService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                HttpResponseMessage result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_config["FMPKey"]}");
                if (result.IsSuccessStatusCode)
                {
                    string content = await result.Content.ReadAsStringAsync();
                    FMPStock[] tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    FMPStock stock = tasks[0];

                    if (stock != null)
                    {
                        return stock.ToStockFromFMPStock();
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
