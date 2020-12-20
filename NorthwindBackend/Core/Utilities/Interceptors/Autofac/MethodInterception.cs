using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors.Autofac
{
    // Bir methodu nasıl yorumlayacağını anllattığımız yerdir.
    public abstract class MethodInterception:MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) // OnBefore metodun çalışmasının önünde.. metod çalışmadan önce burası çalışacaktır.
        {
            // Invocation çalıştırılmaya çalışan operasyondur.
        }
        protected virtual void OnAfter(IInvocation invocation) //  metodun çalışmasının sonrasında.. metod çalıştıktan sonra burası çalışacaktır.
        {
            // Invocation çalıştırılmaya çalışan operasyondur.
        }
        protected virtual void OnException(IInvocation invocation, System.Exception e) // metod hata verince burası çalışacaktır.
        {
            // Invocation çalıştırılmaya çalışan operasyondur.
        }
        protected virtual void OnSuccess(IInvocation invocation) // metod başarılı ise burası çalışacaktır.
        {
            // Invocation çalıştırılmaya çalışan operasyondur.
        }
        // virtual imzalanmış Intercept metodu override edilir.
        public override void Intercept(IInvocation invocation) // Nerde nasıl yorumlayacaktır ? Bir metodu nasıl ele alacağız ?
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed(); // metodu çalıştır.
            }
            catch (Exception e)
            {

                isSuccess = false;
                OnException(invocation,e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
