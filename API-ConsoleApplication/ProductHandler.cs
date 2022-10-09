using API_ConsoleApplication.ProductStock;
using APIBusinessLogic;
using APIBusinessLogic.Orders.Contracts;
using APIBusinessLogic.Stocks.Contracts;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;

namespace API_ConsoleApplication
{
    public class ProductHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IProductOrderService _productorderservice;
        private readonly IProductStockService _productstockservice;
        public ProductHandler(IProductOrderService productorderservice, IProductStockService productstockservice, IConfiguration configuration)
        {
            _productorderservice = productorderservice;
            _configuration = configuration;
            _productstockservice = productstockservice;
        }
        public async Task RunAsync()
        {
            string productnumber = string.Empty;

            try
            {
                //Getting the config details
                var config = _configuration.GetSection("APIConfigurations").Get<APIConfigDetails>();

                //
                if (config != null)
                    productnumber = await ProductOrderHandler.GetTopFiveProductOrderDetails(_productorderservice, config);

                //
                if (!string.IsNullOrEmpty(productnumber))
                    await ProductStockHandler.ProductUpdateStockDetails(_productstockservice, config, productnumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
