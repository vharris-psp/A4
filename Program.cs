using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using A4.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.FileSystemGlobbing;
namespace A4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Apply migrations and seed data
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<A4DbContext>();
                    dbContext.Database.Migrate(); // Apply pending migrations
                    // Additional seed data logic if needed
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while applying migrations or seeding the database.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => 
            {
                webBuilder.ConfigureServices( services => 
                {
                    services.AddControllersWithViews();
                });
                webBuilder.Configure(app => 
                {
                    app.UseRouting();
                    app.UseStaticFiles();
                    app.UseEndpoints(endpoints => 
                    {
                        endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Dashboard}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute(
                            name: "deleteUser",
                            pattern: "User/Delete/{id}",
                            defaults: new { controller = "User", action = "Delete"}
                        );
                        endpoints.MapControllerRoute(
                            name: "po",
                            pattern: "PO/{action=Create}/{id?}",
                            defaults: new { controller = "PO" }
                        );
                        
                    });
                });
            })
                .ConfigureServices((hostContext, services) => 
                {
                    services.AddDbContext<A4DbContext>(options => 
                    {
                        options.UseSqlite("Data Source=app.db");
                    });
                });
    }
}
