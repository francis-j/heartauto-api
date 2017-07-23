using System;

namespace DAL.Loggers
{
    public class EventLogger : IErrorLogger
    {
        public void Write(string source, string message)
        {
            var logger = new EventLogger();
            logger.Write(source, message);
        }
    }
}