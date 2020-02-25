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
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) // Sisteme autofac eklendi. Varsay�lan yap� ile birlikte Autofac'de kullan�lacakt�r.
                .ConfigureContainer<ContainerBuilder>(builder => 
                {
                    builder.RegisterModule(new AutofacBusinessModule()); // Autofac'in kullan�laca�� mod�l belirtildi.
                    // Art�k bu i�lem yap�ld�ktan sonra url ile gerekli veriler al�nabilecektir.
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
