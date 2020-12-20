using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogParameter
    {
        public string Name { get; set; } // Örneğin Product instance'ın productId
        public object Value { get; set; } // instance'ın değeri (örnegin productId'nin 5 değeri)
        public string Type { get; set; } // (Type: Product, Int32 etc)
    }
}
