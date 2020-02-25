using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.AutoFac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) // Sisteme autofac eklendi. Varsayýlan yapý ile birlikte Autofac'de kullanýlacaktýr.
                .ConfigureContainer<ContainerBuilder>(builder => 
                {
                    builder.RegisterModule(new AutofacBusinessModule()); // Autofac'in kullanýlacaðý modül belirtildi.
                    // Artýk bu iþlem yapýldýktan sonra url ile gerekli veriler alýnabilecektir.
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
