using Castle.DynamicProxy;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect:MethodInterception
    {
        // ortaya çıkan zaman hesaplanacaktır.
        private int _interval; // geçen süreyi tutacak olan değişken. Kullanıcıdan alınacak bu nedenle initialize edilecektir.
        private Stopwatch _stopwatch; // kronometre, CoreModule'ın içine eklendi.

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }
        protected override void OnBefore(IInvocation invocation)
        {
            // İşlem başladığında stopwatch'ın başlatılması gerekmektedir.
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds>_interval) // Saniye cinsinden geçen süre interval'dan büyükse; 
            {
                Debug.WriteLine($"Performance: {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}--> {_stopwatch.Elapsed.TotalSeconds}"); // ilgili kişiye mail bile atabiliriz ya da log yazabiliriz.
            }
            _stopwatch.Reset();
        }
    }
}
