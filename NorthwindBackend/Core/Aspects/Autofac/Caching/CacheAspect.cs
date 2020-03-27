using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect:MethodInterception
    {
        // Öncelikli olarak duration'a ihtiyacımız var. Datayı ne kadar Cache içerisinde tutacağız.
        // İkinci olarak cache manager'ın kendisine ihtiyacımız var.
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration=60) // değer gelmez ise 60 dakika default olarak verilecektir.
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        // Operasyon bazlı caching altyapısı gerçekleştirilecektir. Metoda farklı parametreler gelirse key değişecektir.
        public override void Intercept(IInvocation invocation)
        {
            // cache'de var mı varsa cache'den getir yoksa ekle.
            // key değeri metod bilgisi olacak. Class'ı parametre bilgileri vs.
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); // {Class}.{Method}, Burası key değerinin başlangıcı oldu.
            var arguments = invocation.Arguments.ToList(); // bunlar argümanlar.
            var key = $"{methodName}({string.Join(",",arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.IsAdd(key)) // bu key var ise.
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed(); // if bloguna girmez ise metodu çalıştır.
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
