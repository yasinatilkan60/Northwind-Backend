using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.AutoFac
{
    // Modüller, interface'lerin karşılığını belirttiğimiz dosylardır.
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder) // Load bizim konfigürasyonu yaptığımız yer olacaktır. Container Builder Autofac'den gelir.
        {
            builder.RegisterType<ProductManager>().As<IProductService>();  // Eğer ctorun'da IProductService istenirse, ona ProductManager verilecektir.
            builder.RegisterType<EfProductDal>().As<IProductDal>(); // Eğer ctorun'da IProductDal istenirse, ona EfProductDal verilecektir.
            
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<UserManager>().As<IUserService>();  
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            // Yukardaki operasyonlar için bir tane interceptor çalıştırılmalıdır.
            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); // mevcut assembly'e ulaşıldı.

            // Proxy araya girmedir.
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()// araya girecek olan nesne.
                }).SingleInstance(); // tek bir instance oluşsun.
        }
    }
}
