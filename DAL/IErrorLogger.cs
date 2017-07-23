using System;

namespace DAL
{
    public interface IErrorLogger
    {
        void Write(string source, string message);
    }
}