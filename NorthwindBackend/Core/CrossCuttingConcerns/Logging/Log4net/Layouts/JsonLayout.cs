using log4net.Core;
using log4net.Layout;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.Log4net.Layouts
{
    // log4net.config içindeki konfigürasyona göre oluşturuldu.
    public class JsonLayout : LayoutSkeleton // LayoutSkeleton log4net üzerindeki base sınftır. 
    {
        public override void ActivateOptions()
        {
            // Daha farklı teknikler içindir. Burada boş kalacaktır.
        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            var logEvent = new SerializableLogEvent(loggingEvent);
            var json = JsonConvert.SerializeObject(logEvent, Formatting.Indented); // Formatting.Indented ile girintilerin olduğu düzenli yapı sağlanır.
            writer.WriteLine(json); // json yazıldı.
        }
    }
}
