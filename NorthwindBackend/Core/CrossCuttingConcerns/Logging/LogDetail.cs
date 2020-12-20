using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string MethodName { get; set; } // Log'la ilgili olan method ismi
        public List<LogParameter> LogParameters { get; set; } // Method'un birden fazla parametreleri olacaktır.
        //public string ExceptionMessages { get; set; } SOLID'in L'sine aykırı. 2 iş benziyor diye bir arada değerlendirilmemelidir.
        // Bu nedenle LogDetailWithException isminde ayrı class açılır.
    }
}
