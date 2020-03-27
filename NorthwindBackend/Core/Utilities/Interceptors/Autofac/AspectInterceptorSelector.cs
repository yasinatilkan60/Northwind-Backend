using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors.Autofac
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAtributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList(); // type Aspect sınıfıdır. Örneğin Product Manager gibi.
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true); // add, getList vs gibi bir method.
            classAtributes.AddRange(methodAttributes);

            return classAtributes.OrderBy(x => x.Priority).ToArray(); // Priority özelliğini verdiğimiz zaman sıralı gelsin.
        }
    }
}
