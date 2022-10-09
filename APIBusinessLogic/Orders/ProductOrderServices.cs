using APIBusinessLogic.Orders.Contracts;
using APIEntities.Enums;
using APIEntities.OrdersEntity;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIBusinessLogic.Orders
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductOrderServices : IProductOrderService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Statuses">List<Product_Statuses></param>
        /// <param name="BaseUrl">string</param>
        /// <param name="OrderApi">string</param>
        /// <param name="apikey"></param>
        /// <returns>var</returns>
        public async Task<Product_CollectionOfReponses> GetOrdersByStatus(List<Product_Statuses> Statuses, string BaseUrl, string OrderApi, string apikey)
        {
            try
            {
                // 
                HttpClient client = new HttpClient();
                var record = new Dictionary<string, string>()
                {
                    ["apikey"] = apikey,
                    ["statuses"] = Statuses.FirstOrDefault().ToString()
                };
                //var uri = QueryHelpers.AddQueryString(BaseUrl + OrderApi, record);
                var results = await client.GetAsync(QueryHelpers.AddQueryString(BaseUrl + OrderApi, record));
                results.EnsureSuccessStatusCode();
                //var jsonString =  results.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product_CollectionOfReponses>(await results.Content.ReadAsStringAsync()) != null ?
                    JsonConvert.DeserializeObject<Product_CollectionOfReponses>(await results.Content.ReadAsStringAsync()) : null;
               // return content;
            }
            catch (HttpRequestException httpex)
            {
                throw httpex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statuses"></param>
        /// <param name="baseUrl"></param>
        /// <param name="OrderApi"></param>
        /// <param name="apikey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductOrderDetails>> GetAllInProgressProducts(List<Product_Statuses> statuses, string baseUrl, 
            string OrderApi, string apikey)
        {
            try
            {
                //Calling Method to get Order
                Product_CollectionOfReponses order_inprogress_details = await GetOrdersByStatus(statuses, baseUrl, OrderApi, apikey);
                
                if (order_inprogress_details != null)
                    return GetTopFiveRecords(order_inprogress_details);
                else
                    return null;
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<ProductOrderDetails> GetTopFiveRecords(Product_CollectionOfReponses responses)
        {
            try
            {
                return responses.Content.SelectMany(x => x.Lines)
                     .GroupBy(productnumber => productnumber.MerchantProductNo)
                     .Select(item => new ProductOrderDetails
                     {
                         ProductNumber = item.FirstOrDefault().MerchantProductNo,
                         Gtin = item.FirstOrDefault().Gtin,
                         Description = item.FirstOrDefault().Description,
                         Quantity = item.Sum(s => s.Quantity)
                     }).OrderByDescending(desc => desc.Quantity).Take(5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
