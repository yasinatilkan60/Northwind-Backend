using Castle.DynamicProxy;
using Core.Aspects.Autofac.Exception;
using Core.CrossCuttingConcerns.Logging.Log4net.Loggers;
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
            // MethodInterceptionBaseAttribute'i implemente edenler listeye eklenir. add, getList vs gibi bir method gibi
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true); // add, getList vs gibi bir method.
            classAtributes.AddRange(methodAttributes);
            classAtributes.Add(new ExceptionLogAspect(typeof(FileLogger))); // FileLogger'a tüm exception'lar yüklenir.
            return classAtributes.OrderBy(x => x.Priority).ToArray(); // Priority özelliğini verdiğimiz zaman sıralı gelsin.
        }
    }
}
