﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.Log4net.Loggers
{
    public class DatabaseLogger:LoggerServiceBase // Db'ye yapacak olduğumuz logdur.
    {
        public DatabaseLogger():base("DatabaseLogger") // DatabaseLogger log4net.config içerisindeki DatabaseLogger'dır.
        {

        }
    }
}
