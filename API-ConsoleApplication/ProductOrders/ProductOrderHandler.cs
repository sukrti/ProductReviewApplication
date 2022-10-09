using APIBusinessLogic;
using APIBusinessLogic.Orders.Contracts;
using APIEntities.Enums;
using APIEntities.OrdersEntity;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API_ConsoleApplication
{
    public static class ProductOrderHandler
    {
        public static async Task<string> GetTopFiveProductOrderDetails(IProductOrderService productorderservice, APIConfigDetails config)
        {
            string productNumber="";
            try
            {
                var topfiveorders = await productorderservice.GetAllInProgressProducts(new List<Product_Statuses> { Product_Statuses.IN_PROGRESS }, config.BaseUrl, config.OrderAPI, config.ApiKey);
                PrintTopFiveProductsOnConsole(topfiveorders.ToArray());               
                while (true)
                {
                    Console.WriteLine("Select a product number to update its stock to 25:");
                    productNumber = Console.ReadLine();
                    if (topfiveorders.Any(c => c.ProductNumber == productNumber)) break;
                    Console.WriteLine("Product not exists!");
                }
               
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return productNumber;
        }
        private static void PrintTopFiveProductsOnConsole(ProductOrderDetails[] topProducts)
        {
            try
            {
                var table = new ConsoleTable();
                IEnumerable<string> alldetails = ProductOrderDetails.GetProductData();
                table.AddColumn(alldetails);
                foreach (var orders in topProducts)
                {
                    table.AddRow(orders.ProductNumber, orders.Gtin, orders.Quantity, orders.Description);
                }
                table.Write();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
