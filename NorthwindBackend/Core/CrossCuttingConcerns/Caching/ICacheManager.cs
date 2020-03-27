using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        // generic yapı kurmalıyız.
        T Get<T>(string key); // Belirli bir tipteki cache değerini okumaya çalışacağız.
        object Get(string key);
        void Add(String key, object data, int duration); // Cache ekleme anahtar, veri ve cache'de durma süresi ile birlikte.
        bool IsAdd(string key); // Eklenmiş mi? Cacheden mi getirilmeli mi?
        void Remove(string key); // belirli bir key'deki cache'i silmeye yarar.
        void RemoveByPattern(string pattern); // Örneğin Get ile başlayan bütün cache'leri sil.
    }
}
