
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    // Bunları Core'a taşıma nedenimiz Core'dan Entities katmanına referans veremememizdir.
    // Çünkü Entities'den Core'a daha önceden referans verdiğimizden dolayı Core'dan Entities referansını veremiyoruz. (JavaScript'te bu mümkündür.)
    public class OperationClaim
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}