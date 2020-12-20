using log4net.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.Log4net
{
    [Serializable] // Serileştirilebilir
    public class SerializableLogEvent // Loglanacak data'nın kendisidir.
    {
        private LoggingEvent _loggingEvent;
        public SerializableLogEvent(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;
        }

        // Loglama datasının içine konması gerekenler;
        public object Message => _loggingEvent.MessageObject;
        // Buraya farklı olarak örneğin loglama işlemini yapacak olan kullanıcı bilgileri vs yazılabilir.
    }
}
