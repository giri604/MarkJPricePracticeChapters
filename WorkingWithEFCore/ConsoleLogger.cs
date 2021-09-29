﻿using System;
using Microsoft.Extensions.Logging;
using static System.Console;

namespace Packt.Shared
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new ConsoleLogger();
        }

        public void Dispose()
        {
            
        }
    }

    public class ConsoleLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Information:
                case LogLevel.None:
                    return false;
                case LogLevel.Debug:
                case LogLevel.Warning:
                case LogLevel.Error:
                case LogLevel.Critical:
                default:
                    return true;
            };
        }
        public void Log<TSate>(LogLevel logLevel, EventId eventId, TSate state, Exception exception, Func<TSate, Exception, string> formatter)
        {
            if (eventId.Id == 20100)
            {
                Write($"Level: {logLevel}, Event ID: {eventId.Id}");

                if (state != null)
                {
                    Write($", Satet: {state}");
                }
                if (exception != null)
                {
                    Write($", Exception: {exception}");
                }
                WriteLine();
            }
        
        }
    }
}
