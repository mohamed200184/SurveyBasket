
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SurveyBasket.api.Middleware;
using SurveyBasket.api.Persistence;
using Serilog;
namespace SurveyBasket.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDependencies(builder.Configuration);
            builder.Host.UseSerilog((context, configuration)=>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            }
              )  ;
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseCors("AllowAll");
            app.UseAuthorization();

          
            app.MapControllers();

            app.UseExceptionHandler();
            app.Run();
        }
    }
}
