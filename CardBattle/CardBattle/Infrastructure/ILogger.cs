using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Infrastructure
{
    public interface ILogger
    {
        void Log(LogLevel level, string message);
    }
}
