using Autofac;
using Business.Abstract;
using Business.Concrete;
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
        protected override void Load(ContainerBuilder builder) // Load bizim konfigürasyonu yaptığımız yer olacaktır.
        {
            builder.RegisterType<ProductManager>().As<IProductService>();  // Eğer ctorun'da IProductService istenirse, ona ProductManager verilecektir.
            builder.RegisterType<EfProductDal>().As<IProductDal>(); // Eğer ctorun'da IProductDal istenirse, ona EfProductDal verilecektir.
            
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<UserManager>().As<IUserService>();  
            builder.RegisterType<EfUserDal>().As<IUserDal>();
        }
    }
}
