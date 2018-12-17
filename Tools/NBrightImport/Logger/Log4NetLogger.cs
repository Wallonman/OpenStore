using System;
using System.Globalization;
using log4net;

namespace ZIndex.DNN.NBrightImport.Logger
{
    /// <summary>
    /// The implementation of the ILog, using the 
    /// log4net library
    /// </summary>
    internal sealed class Log4NetLogger : ILog
    {
        private readonly log4net.ILog _logger;

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="type"></param>
        private Log4NetLogger(Type type) : this(type.FullName)
        {
        }

        static Log4NetLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="name"></param>
        private Log4NetLogger(string name)
        {
            _logger = LogManager.GetLogger(name);
        }

        /// <summary>
        /// Returns an instance of this Logger class
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ILog GetLogger(Type type)
        {
            return new Log4NetLogger(type);
        }

        #region ILog Members

        /// <summary>
        /// Log the message in Debug level
        /// </summary>
        /// <param name="message">the message to log</param>
        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// Log the message in Debug level
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void Debug(string format, params object[] args)
        {
            _logger.DebugFormat(format, args);
        }

        /// <summary>
        /// Log the message in Info level
        /// </summary>
        /// <param name="message">the message to log</param>
        public void Info(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Log the message in Info level
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void Info(string format, params object[] args)
        {
            _logger.InfoFormat(format, args);
        }

        /// <summary>
        /// Log the message in Warn level
        /// </summary>
        /// <param name="message">the message to log</param>
        public void Warn(object message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// Log the message in Warn level
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void Warn(string format, params object[] args)
        {
            Warn((object) String.Format(CultureInfo.CurrentCulture, format, args));
        }

        /// <summary>
        /// Log the message in Fatal level
        /// </summary>
        /// <param name="message">the message to log</param>
        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        /// <summary>
        /// Log the message in Fatal level
        /// </summary>
        /// <param name="message">the message to log</param>
        /// <param name="exception">the exception to log the stack trace</param>
        public void Fatal(string message, Exception exception)
        {
            _logger.Fatal(message, exception);
        }

        /// <summary>
        /// Log the message in Monitor level
        /// </summary>
        /// <param name="message">the message to log</param>
        public void Monitor(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Log the message in Monitor level
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void Monitor(string format, params object[] args)
        {
            _logger.InfoFormat(format, args);
        }

        /// <summary>
        /// Log message object in Monitor level
        /// </summary>
        /// <param name="message">the message to log</param>
        public void Monitor(object message)
        {
            _logger.Info(message);
        }

        #endregion

    }
}