using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        // Burada direkt mesaj geçilir çünkü; işlem zaten başarılıdır.
        public SuccessResult(string message) : base(true, message)
        {

        }
        public SuccessResult() : base(true) // Mesajsız gönderim. success default olarak true değerini alır.
        {

        }

        
    }
}
