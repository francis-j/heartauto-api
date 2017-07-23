using System;

namespace DAL.Loggers
{
    public class ConsoleLogger : IErrorLogger
    {
        public void Write(string source, string message)
        {
            Console.WriteLine("--begin--");
            Console.WriteLine(source);
            Console.WriteLine(message);
            Console.WriteLine("--end--");
        }
    }
}