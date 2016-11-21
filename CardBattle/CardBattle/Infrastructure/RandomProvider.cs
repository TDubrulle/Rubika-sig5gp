using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Infrastructure
{
    public class RandomProvider
    {
        public readonly Random _random = new Random();

        public Random Random
        {
            get
            {
                return _random;
            }
        }
    }
}
