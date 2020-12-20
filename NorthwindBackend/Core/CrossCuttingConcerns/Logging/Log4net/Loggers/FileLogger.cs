using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.Log4net.Loggers
{
    public class FileLogger : LoggerServiceBase // Db'ye yapacak olduğumuz logdur.
    {
        public FileLogger() : base("JsonFileLogger") // DatabaseLogger log4net.config içerisindeki DatabaseLogger'dır.
        {
            
        }
    }
}
