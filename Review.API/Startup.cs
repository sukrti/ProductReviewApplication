using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Review.API.Contracts;
using Review.API.Models;
using Review.API.Services;
using System.Text.Json.Serialization;

namespace Review.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services
                 .AddDbContext<ReviewDBContext>(options =>
                 options.UseInMemoryDatabase(Configuration.GetConnectionString("Database")))
                   .AddScoped<IReviewService, ReviewService>()

                .AddMvc(a => { a.EnableEndpointRouting = false; })
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))

              
             
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
                
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "Review API", Version = "v1"});
              
            });
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMvc();
            app.UseSwagger();
           
            app.UseSwaggerUI(c =>
            {
               //c.RoutePrefix = "swagger/ui";
                c.SwaggerEndpoint("v1/swagger.json", "Review API(v1)");
            });
        }
    }
}