using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    // .NET Core'daki ServiceCollection Nesnesini Extension edeceğiz.
    public static class ServiceCollectionExtensions
    {
        // Bütün merkezi konfigürasyonlar burada eklenecektir.
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, ICoreModule[] modules)
        {
            foreach(var module in modules)
            {
                module.Load(services); // Bütün modülleri .net core'a ekleyeceğiz.
            }
            return ServiceTool.Create(services);
        }
    }
}
