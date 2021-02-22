using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PruebaPWC.Entities;
using PruebaPWC.Persistence;

namespace PruebaPWC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostserver = CreateHostBuilder(args).Build();

            using (var ambiente = hostserver.Services.CreateScope())
            {
                var services = ambiente.ServiceProvider;
                try
                {
                    var userManaget = services.GetRequiredService<UserManager<Usuario>>();
                    var context = services.GetRequiredService<PruebaBdContext>();
                    context.Database.Migrate();
                    DataTest.Insertar(context, userManaget).Wait();
                }
                catch (Exception e)
                {
                    var logging = services.GetRequiredService<ILogger<Program>>();
                    logging.LogError(e, "Error en migracion");
                }

                hostserver.Run();

            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
