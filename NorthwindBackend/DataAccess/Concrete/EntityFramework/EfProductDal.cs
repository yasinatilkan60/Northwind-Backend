using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // EfEntityRepositoryBase içindeki operasyonlar implemente edildi. Bu nedenle IProductDal implementi hata vermedi.
    public class EfProductDal : EfEntityRepositoryBase<Product,NorthwindContext>,IProductDal 
    {
        // Buraya yazılacak crud işlemleri diğerlerinde de tekrarlanacağı için Core (Framerwork) katmanında Repository Base yazılacak ve
        // hepsinde ortak kullanılacaktır.

    }
}
