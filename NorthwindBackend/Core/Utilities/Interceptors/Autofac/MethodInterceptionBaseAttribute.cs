using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors.Autofac
{
    // Class'larda, Method'larda, birden fazla ve inherit edildiğinde kullanılabilir.
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, AllowMultiple = true, Inherited = true)] // Attribute kısıtları
    public abstract class MethodInterceptionBaseAttribute:Attribute, IInterceptor
    {
        public int Priority { get; set; } // aspect işlemlerinin (loglama, validation vs) sırasını belirlemek için kullanılacaktır.

        public virtual void Intercept(IInvocation invocation) // virtual olarak imzalamamızın sebebi metodu değiştirebilmek için.
        {
            
        }
    }
}
