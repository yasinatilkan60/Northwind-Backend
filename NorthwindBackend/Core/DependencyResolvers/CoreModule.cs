using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DependencyResolvers
{
    //Startup.cs içerisinde eklemek istediğim merkezi olan middlewear'ı artık buraya ekleyeceğiz.
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache(); // Servis koleksiyonuna AddMemoryCache eklendi.
            services.AddSingleton<ICacheManager, MemoryCacheManager>(); // ICacheManager istenirse, MemoryCacheManager verilecektir.
            // Örneğin microsoft yerine başka bir sisteme geçilirse MemoryCacheManager yerine başka manager yazılması yeterli olacaktır.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // bu sayede artık user'a erişebileceğiz.
        }
    }
}
