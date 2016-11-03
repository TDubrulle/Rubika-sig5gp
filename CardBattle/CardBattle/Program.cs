using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardBattle.Models;

namespace CardBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            var spadesAce = new Card(Values.Ace, Suit.Spades);
            var spadesAce2 = new Card(Values.Seven, Suit.Spades);
            Console.WriteLine(spadesAce.Equals(spadesAce2));

            var dealer = new CardDealer();

            /*for (var i = 0; i < 100; i++)
            {
                var randomCard = dealer.RandomCard();

                Console.WriteLine("I drew a card: " + randomCard);
            }*/

            Console.ReadLine();
        }
    }
}
