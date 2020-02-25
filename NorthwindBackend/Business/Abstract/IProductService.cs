using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<Product> GetById(int productId);
        IDataResult<List<Product>> GetList();  // Buraya Expression geçilmez.  Expression servis katmanında tamamlanır.
        IDataResult<List<Product>> GetListByCategory(int categoryId);
        IResult Add(Product product); // Data döndürmek istemiyorum, Başarılı mı oldum ? Başarısız mı oldum ?  
        IResult Delete(Product product);
        IResult Update(Product product); 
        
    }
}
