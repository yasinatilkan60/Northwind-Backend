﻿using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Exception;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4net.Loggers;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    // Sektörde Manager yerine Service'de yazılmaktadır.
    // İşlemleri yapabilmek için DataAccess (Veri erişim) katmanının çağrılması gerekir.

    // Neden interface kullanılıyor ? İlgili interface'i implemente edebilen tüm yapıları geri dönderebilmek için.
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal) // Dependency Injection; IProductDal'ı kim implemente ediyorsa onu verebilirim. (Artık orm vs gibi yapılara bağımlılık kalmadı.)
        {

            _productDal = productDal;
            
        }

        // Cross Cutting Concerns -> Uygulamayı dikine kesen ilgi alanları. ( Validation, Cache, Loglama, Performans, Auth, Transaction)
        // AOP bir pattern DEĞİLDİR. Yazılım geliştirme yaklaşımıdır.
        // AOP - Aspect Oriented Programming -> CCC'ler için kullanılır. Başka bir şey için kullanılmaz.
        // [ValidationAspect(typeof(ProductValidator), Priority = 2)] // Priority 2 olduğu için daha sonra burası çalışır. 
        [ValidationAspect(typeof(ProductValidator), Priority = 1)] // Kısaca bu işlem parametrede nesneyi (product) bulur ve map eder. Daha sonra validate işlemini gerçekleştirir.
        [CacheRemoveAspect(pattern:"IProductService.Get")] // Cache'de Get Operasyonları varsa onları sil.
        public IResult Add(Product product)
        {
            //if(_productDal.Get(p => p.ProductName == product.ProductName) != null) // Bunun burada burada yapılması problemdir.
            //{ // Bu kod başka bir yerde de yazılabilir, değiştirilebilir her yere aynı kod mu yazılacak ?
            //    return new ErrorResult(Messages.ProductNameAlreadyExists);
            //}
            // ValidationTool.Validate(new ProductValidator(), product); // Bu yöntemde tam olarak uygun değildir.
            // Business code yazılacak
            //IResult result = CheckIfProductNameExists(product.ProductName); // 2. hata
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName)); // Birden fazla logic de gönderebilirdik.
            if (result !=null) // Bu da problemdir çünkü her yerde aynı kodu tekrarlayacağız.
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded); // magic string
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetList(p => p.ProductName == productName).Any();
            if (result) 
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        public IResult Delete(Product product)
        {
            // Business code yazılacak (Silinmemesi gereken bir ürün olabilir.)
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted); // magic string
        }

        public IDataResult<Product> GetById(int productId)
        {
            //EfProductDal productDal = new EfProductDal(); // Bu bağımlılığı (EF'ye) soyutlamak gerekir. Bu DependencyInjection ile yapılır.
            //return productDal.Get(p => p.ProductId == productId);
            // Business code yazılacak
            
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }
        //[ExceptionLogAspect(typeof(FileLogger))] Tümüne yazmak gerektiğinden tercih edilmez.
        [PerformanceAspect(interval:5)]
        public IDataResult<List<Product>> GetList()
        {
            // Business code yazılacak
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }
        //[SecuredOperation("Product.List,Admin")] // Priority yazmaksak üstteki ilk çalışır. Aşağıdaki işlemler için bu yetkiler olmalıdır. 
        //[CacheAspect(duration:10)] // 10 dakika boyunca cache'den gelecek. Sonra yine cache'e eklenecektir.
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            // Business code yazılacak
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }
        [TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {
            // Bir product'ı eklemeye çalışacağız. Daha sonra işlemin hata durumuna senaryo ile bakacağız.
            _productDal.Update(product); // Bu işlem başarılı olsun.
            //_productDal.Add(product); // Bu işlem başarısız olsun ve Update işlemi geri alınsın.
            return new SuccessResult(Messages.ProductUpdated);
        }

        public IResult Update(Product product)
        {
            // Business code yazılacak
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated); // magic string
        }
    }
}
