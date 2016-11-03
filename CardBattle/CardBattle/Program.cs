﻿using System;
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

            Console.WriteLine("I created a card: " + spadesAce);

            var dealer = new CardDealer();

            for (var i = 0; i < 6; i++)
            {
                var hand = dealer.Deal(5);
                hand.Sort();

                Console.WriteLine("my hand contains " + string.Join(", ", hand.Select(c => c.ToString()).ToArray()));
            }

            //for (var i = 0; i < 100; i++)
            //{
            //    var randomCard = dealer.RandomCard();

            //    Console.WriteLine("I drew a card: " + randomCard);
            //}


            Console.ReadLine();
        }
    }
}
