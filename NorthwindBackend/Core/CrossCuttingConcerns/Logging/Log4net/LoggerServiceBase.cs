using log4net;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Core.CrossCuttingConcerns.Logging.Log4net
{
    public class LoggerServiceBase
    {
        private ILog _log; // loglamayı yapacak olan nesne.
        // Logger Appender devreye koyulacaktır.
        public LoggerServiceBase(string name) // name loglama tipdir. Veritbanı mı ? File mı?
        {
            // Config dosyasını okumak gerekir. Bunun için XmlDocument kullanılır.
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(File.OpenRead("log4net.config"));

            ILoggerRepository loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument["log4net"]);

            _log = LogManager.GetLogger(loggerRepository.Name, name); // Yukarıdaki konfigürasyona ve gelen log tipine göre log al.
        }
        // Log işlemlerini aktif ya da pasif hale getirebiliriz. Amaç; sistemi hızlandırmaktır.
        public bool IsInfoEnabled => _log.IsInfoEnabled; // Konfigürasyonda info açık mı ?

        public bool IsDebugEnabled => _log.IsDebugEnabled;
        public bool IsWarnEnabled => _log.IsWarnEnabled;
        public bool IsFatalEnabled => _log.IsFatalEnabled;
        public bool IsErrorEnabled => _log.IsErrorEnabled;

        public void Info(object logMessage) // logMessage log datasıdır.
        {
            if (IsInfoEnabled)
                _log.Info(logMessage);
        }
        public void Debug(object logMessage)
        {
            if (IsDebugEnabled)
                _log.Debug(logMessage);
        }
        public void Warn(object logMessage)
        {
            if (IsWarnEnabled)
                _log.Warn(logMessage);
        }
        public void Fatal(object logMessage)
        {
            if (IsFatalEnabled)
                _log.Fatal(logMessage);
        }
        public void Error(object logMessage)
        {
            if (IsErrorEnabled)
                _log.Error(logMessage);
        }
    }
}
