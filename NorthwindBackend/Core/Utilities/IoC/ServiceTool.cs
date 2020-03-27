using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    // Bu yapı sayesinde .NET Core'un kendi servislerine erişebiliyoruz.
    public static class ServiceTool
    {
        // Uygulamanın service collection'ındaki service provider'ına erişmeye çalışacağız.
        // Sistemin merkezi bu tool sayesinde kontrol edilecektir.
        public static IServiceProvider ServiceProvider { get; set; } // Merkezi servis yönetim nesnesidir.

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
