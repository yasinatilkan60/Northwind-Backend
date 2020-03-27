using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        private IMemoryCache _cache;
        public MemoryCacheManager()
        {
            _cache = ServiceTool.ServiceProvider.GetService<IMemoryCache>(); // .Net Core ekibinin Memory Cache implementasyonu.
        }
        public void Add(string key, object data, int duration)
        {
            _cache.Set(key, data, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            // Memory Cache Manager altyapıyı kendisi sağlar.
            return _cache.Get<T>(key); // İlgili anahtarı verdiğimiz değeri okuyabiliriz.
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _cache.TryGetValue(key, out _); // out _ ile değer döndürme önemsenmeyip sadece var mı yok mu ona bakılmıştır.
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            // EntriesCollection bir cache koleksiyonudur.
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_cache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>(); // ICache entry her bir cache girişidir.
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null); // Her bir cache değeri okundu.
                cacheCollectionValues.Add(cacheItemValue); // Her bir cache koleksiyonun içine atıldı.
            }
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase); // pattern'a uygun olan regex oluşturulur. 
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList(); // regex'e uygun olan bulunup liste haline getirilir.

            foreach (var key in keysToRemove)
            {
                _cache.Remove(key); // gönderdiğimiz pattern'a uygun olan cache'ler tek tek silinecektir.
            }
        }
    }
}
