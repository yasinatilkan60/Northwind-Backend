using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    // Bunları Core'a taşıma nedenimiz Core'dan Entities katmanına referans veremememizdir.
    // Çünkü Entities'den Core'a daha önceden referans verdiğimizden dolayı Core'dan Entities referansını veremiyoruz. (JavaScript'te bu mümkündür.)
    public class User: IEntity 
    {
        public int Id { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
    }
}
