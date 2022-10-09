using System.Net.Http;
using System.Threading.Tasks;

namespace APIBusinessLogic.Stocks.Contracts
{
    public interface IProductStockService
    {
        /// <summary>
        /// This method takes the product number and configuration details and update the stock to 25 using API
        /// </summary>
        public Task<HttpResponseMessage> UpdateProductStock(string productNumber, int stock, string baseUrl, string stockApi, string apikey);
    }
}
