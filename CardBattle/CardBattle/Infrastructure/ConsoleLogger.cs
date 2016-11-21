using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Infrastructure
{
    public class ConsoleLogger : ILogger
    {
        public LogLevel MinLevel { get; set; }

        public void Log(LogLevel level, string message)
        {
            if(level <= MinLevel)
            {
                Console.WriteLine(message);
            }
        }
    }
}
