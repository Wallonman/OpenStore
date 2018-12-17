using System;

namespace ZIndex.DNN.NBrightImport.Logger
{
    /// <summary>
    /// Logger interface
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Log the message in Debug level
        /// </summary>
        /// <param name="message">the message to log</param>
        void Debug(string message);

        /// <summary>
        /// Log the message in Debug level
        /// </summary>
        /// <param name="message">the message to log</param>
        /// <param name="args">arguments used to format the message</param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Log the message in Fatal level
        /// </summary>
        /// <param name="message">the message to log</param>
        void Fatal(string message);

        /// <summary>
        /// Log the message in Fatal level
        /// </summary>
        /// <param name="message">the message to log</param>
        /// <param name="exception">the exception to log the stack trace</param>
        void Fatal(string message, Exception exception);

        /// <summary>
        /// Log the message in Info level
        /// </summary>
        /// <param name="message">the message to log</param>
        void Info(string message);

        /// <summary>
        /// Log the message in Info level
        /// </summary>
        /// <param name="message">the message to log</param>
        /// <param name="args">arguments used to format the message</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Log the message in Warn level
        /// </summary>
        /// <param name="message">the message to log</param>
        void Warn(object message);

        /// <summary>
        /// Log the message in Warn level
        /// </summary>
        /// <param name="message">the message to log</param>
        /// <param name="args">arguments used to format the message</param>
        void Warn(string message, params object[] args);


    }
}