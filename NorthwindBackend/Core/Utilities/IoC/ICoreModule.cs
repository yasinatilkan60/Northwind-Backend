using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection collection); // .Net Core IServiceCollection'un ne olduğunu bildiği için ona göre enjeksiyon işlemini gerçekleştirecektir.
        // ICoreModule'ı Core içerisinde DependencyResolvers klasörü içinde implemente edelim.
    }
}
