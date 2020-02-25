using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    // t yerine referans tip olan her şey konulabilir. 
    //Veritabanına IEntity'den türetilmiş her şey gönderilebilir fakat; new'lenebilir olması gerekmektedir.
    public interface IEntityRepository<T> where T:class, IEntity,new() 
    {
        T Get(Expression<Func<T, bool>> filter); // Expression Linq ile herhangi bir parametre alabilirim.
        IList<T> GetList(Expression<Func<T, bool>> filter=null); // filtre gönderilmezse hepsi listelenir. 
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
