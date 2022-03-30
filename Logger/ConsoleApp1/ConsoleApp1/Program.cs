using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApp1
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex);
    }
    public class LocalFileLogger<T> : ILogger
    {
        string way;
        private string GenericTypeName => typeof(T).Name;

        public LocalFileLogger(string way)
        {
            this.way = way;
        }
        public void LogInfo(string message)
        {
            Write($"[Info]: [{GenericTypeName}] : {message}");
        }
        public void LogWarning(string message)
        {
            Write($"[Warning] : [{GenericTypeName}] : {message}");
        }
        public void LogError(string message, Exception ex)
        {
            Write($"[Info]: [{GenericTypeName}] : {message}");
        }
        private void Write(string message)
        {
            using (StreamWriter w = new StreamWriter(way, true))
            {
                w.WriteLine(message);

            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LocalFileLogger<string> first = new LocalFileLogger<string>("first.txt");
            //LocalFileLogger<int> second = new LocalFileLogger<int>("second.txt");
            List<ILogger> loggers = new List<ILogger>()
            {
                 new LocalFileLogger<string>("first.txt"),
                 new LocalFileLogger<int>("second.txt")
            };
            foreach (var item in loggers)
            {
                item.LogError("gkgkg",new Exception ("test"));

            }
        }
    }
}
