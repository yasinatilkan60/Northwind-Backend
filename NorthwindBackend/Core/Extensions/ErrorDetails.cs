using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public override string ToString() // JsonConvert / Nesne serileştirme işlemi yapılmıştır.
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
