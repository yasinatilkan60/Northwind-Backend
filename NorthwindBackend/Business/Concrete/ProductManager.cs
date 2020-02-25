using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public IResult Add(Product product)
        {
            // Business code yazılacak
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded); // magic string
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

        public IDataResult<List<Product>> GetList()
        {
            // Business code yazılacak
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            // Business code yazılacak
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }

        public IResult Update(Product product)
        {
            // Business code yazılacak
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated); // magic string
        }
    }
}
