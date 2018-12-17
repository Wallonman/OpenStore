namespace ZIndex.DNN.NBrightImport.Logger
{
    /// <summary>
    /// Base class to handle the logging with log4net. This class implements the <see cref="ILogger"/>
    /// interface. Extend this class to get access to all log4net features
    /// </summary>
    public class LoggerBase
    {
        

        /// <summary>
        /// Returns the logger
        /// </summary>
        public virtual ILog Logger { get; private set; }
        


        /// <summary>
        /// Construct the Logger, using the log4net.config file as configurator
        /// </summary>
        public LoggerBase()
        {
            Logger = Log4NetLogger.GetLogger(GetType());
        }

        /// <summary>
        /// Logger's constructor using the given type as logger name
        /// </summary>
        /// <param name="type"></param>
        public LoggerBase(System.Type type)
        {
            Logger = Log4NetLogger.GetLogger(type);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ILog StaticLogger(System.Type type)
        {
            return Log4NetLogger.GetLogger(type);
        }

       
    }
}
