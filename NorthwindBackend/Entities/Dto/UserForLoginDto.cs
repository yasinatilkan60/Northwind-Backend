using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    // Arayüzü ilgilendiren bir model olmadığı için Dto Entities katmanına konmuştur.
    public class UserForLoginDto: IDto 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
