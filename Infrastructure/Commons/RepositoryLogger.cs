using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Commons
{
    public class RepositoryLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            // Console.WriteLine("BeginScope Test");
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // Console.WriteLine("IsEnabled Test");
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // Console.WriteLine("Log Test");
        }
    }
}
