using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal: IEntityRepository<Product> // IProductDal içerisinde IEntityRepository içerisindeki bütün operasyonlar vardır.
    {
        // interface interface'i implemente edebilir.

    }
}
