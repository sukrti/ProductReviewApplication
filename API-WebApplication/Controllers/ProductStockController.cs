using APIBusinessLogic;
using APIBusinessLogic.Stocks.Contracts;
using APIEntities.StockEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_WebApplication.Controllers
{
    [ApiController]

    [Route("api/v1/[controller]/[action]")]
    public class ProductStockController : ControllerBase
    {
        private readonly IProductStockService _service;
        private readonly IConfiguration _config;
        public ProductStockController(IProductStockService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateStockData(ProductStockDetails productstockdetails)
        {
            try
            {
                var config = _config.GetSection("APIConfigurations").Get<APIConfigDetails>();
                return await _service.UpdateProductStock(productstockdetails.MerchantProductNo, 25, config.BaseUrl, config.StockAPI, config.ApiKey);
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
