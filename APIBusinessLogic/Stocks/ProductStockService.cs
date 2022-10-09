using APIBusinessLogic.Stocks.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using APIEntities.StockEntity;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.WebUtilities;

namespace APIBusinessLogic.Stocks
{
    public class ProductStockService : IProductStockService
    {
        public async Task<HttpResponseMessage> UpdateProductStock(string productNumber,int stock, string baseUrl, string stockApi, string apikey)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                List<ProductStockDetails> productstockdetails = new List<ProductStockDetails>()
                {
                    new ProductStockDetails
                    {
                        MerchantProductNo = productNumber,
                        Stock = stock
                    }
                };
                var records = new Dictionary<string, string>()
                {
                    ["apikey"] = apikey,
                };
                var url = QueryHelpers.AddQueryString(baseUrl + stockApi, records);
                var productdetails = JsonConvert.SerializeObject(productstockdetails);
                var requestContent = new StringContent(productdetails, Encoding.UTF8, "application/json");
                var uri = Path.Combine(url);
                var response = await httpClient.PutAsync(uri, requestContent);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch(HttpRequestException ex)
            {
                throw ex;
            }

        }
    }
}
