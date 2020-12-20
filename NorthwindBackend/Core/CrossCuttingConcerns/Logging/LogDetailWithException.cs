using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetailWithException:LogDetail
    {
        public string ExceptionMessages { get; set; }
    }
}
