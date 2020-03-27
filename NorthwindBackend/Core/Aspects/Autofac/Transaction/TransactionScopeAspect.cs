using Castle.DynamicProxy;
using Core.Utilities.Interceptors.Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        // Burada MethodInterception içerisindeki Intercept override edilecektir.

        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope()) // Transaction yazabilmemiz için TransactionScope kullanılmalıdır.
            {
                // Disposable Pattern için using kullanılmıştır.
                try
                {
                    invocation.Proceed(); // metodu çalıştır.
                    transactionScope.Complete(); // işlemi kabul et ve çalıştır.
                }
                catch (Exception)
                {
                    transactionScope.Dispose(); // Başarılı değil ise yapılan işlemleri geri al ve hata fırlat.
                    throw;
                }
            }
        }
    }
}
