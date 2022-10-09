// See https://aka.ms/new-console-template for more information
using APIBusinessLogic.Orders.Contracts;
using APIBusinessLogic.Orders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using APIBusinessLogic.Stocks.Contracts;
using APIBusinessLogic.Stocks;
using API_ConsoleApplication;
using System.Threading.Tasks;
using System;

class program
{
  public  static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();    
        var services = ActivatorUtilities.CreateInstance<ProductHandler>(host.Services);
        Task task= services.RunAsync();
        task.Wait();
        Console.ReadLine();
        
    }
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, configuation) =>
            {
            configuation.Sources.Clear();
            configuation.AddJsonFile("Dev.appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {        
            services.AddScoped<IProductOrderService, ProductOrderServices>();
            services.AddScoped<IProductStockService, ProductStockService>();
         
            services.AddOptions();
            });       
    }
}












