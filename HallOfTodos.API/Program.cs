using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HallOfTodos.API.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace HallOfTodos.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder
                    .ConfigureNLog("nlog.config")
                    .GetCurrentClassLogger();
            try
            {

                logger.Info("Initializing application...");
                var host = CreateHostBuilder(args).Build();

                //using (var scope = host.Services.CreateScope())
                //{
                //    try
                //    {
                //        var context = scope.ServiceProvider.GetService<TodoContext>();

                //        // for demo purposes, delete the database & migrate on startup so 
                //        // we can start with a clean slate
                //        context.Database.EnsureDeleted();
                //        context.Database.Migrate();
                //    }
                //    catch (Exception ex)
                //    {
                //        logger.Error(ex, "An error occurred while migrating the database.");
                //    }
                //}

                // run the web app
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Application stopped because of exception.");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog();
    }
}
