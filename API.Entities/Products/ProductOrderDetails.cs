using System.Collections.Generic;
namespace APIEntities.OrdersEntity
{
    public class ProductOrderDetails
    {
        public string ProductNumber { get; set; }
        public string Gtin { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public static IEnumerable<string> GetProductData()
        {
            return new[] { "Product Number", "Gtin", "Quantity", "Description" };
        }
    }
}
