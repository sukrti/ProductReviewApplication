using System.Collections.Generic;
namespace APIEntities.OrdersEntity
{
  public class Product_CollectionOfReponses
    {
        public List<Product_Contents> Content { get; set; }
     
        public int TotalCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int StatusCode { get; set; }

        public string? LogId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public Dictionary<string, List<string>> ValidationErrors { get; set; }

    }
}
