using CardBattle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardBattle
{
    public class CardDealer
    {
        private readonly Random _rand = new Random();
        private static readonly int _suitsCount = Enum.GetValues(typeof(Suit)).Length;
        private static readonly int _valuesCount = Enum.GetValues(typeof(Values)).Length;

        private List<Card> deck = new List<Card>();

        /// <summary>
        /// Pick a card randomly from all possible cards. They are not drawn from a deck.
        /// </summary>
        /// <returns>A random card.</returns>
        public Card RandomCard()
        {
            var suit = (Suit)_rand.Next(_suitsCount);
            var value = (Values)_rand.Next(_valuesCount);
            return new Card(value, suit);
        }

        public bool canDeal(int n)
        {
            return deck.Count < n;
        }

        public int deckCount
        {
            get { return deck.Count; }
        }

        /// <summary>
        /// Deal n cards from a deck.
        /// </summary>
        /// <param name="n">The number of cards to pick from the deck.</param>
        /// <returns>A list of cards from the cardDealer's current deck.</returns>
        public List<Card> deal(int n)
        {
            if(canDeal(n))
            {
                throw new IndexOutOfRangeException("Not enough cards available");
            }
            List<Card> r = new List<Card>(n);
            for (int i = 0; i < n; ++i)
            {
                r.Add(pickRandomCardFrom(deck));
            }
            return r;
        }

        public void shuffleDeck()
        {
            deck = generateDeck();
        }

        protected List<Card> generateDeck()
        {
            List<Card> newDeck = new List<Card>(_suitsCount * _valuesCount);
            for(int i = 0; i < _valuesCount; ++i)
            {
                for(int j = 0; j < _suitsCount; ++j)
                {
                    newDeck.Add(new Card((Values)i, (Suit)j));
                }
            }
            return newDeck;
        }

        /// <summary>
        /// pick a random card from the card list.
        /// </summary>
        /// <param name="deck">The list of card to take the card from.</param>
        /// <returns>the picked card.</returns>
        protected Card pickRandomCardFrom(List<Card> deck)
        {
            if (deck.Count > 0)
            {
                int random = _rand.Next(0, deck.Count);
                Card r = deck[random];
                deck.RemoveAt(random);
                return r;
            } else
            {
                return null;
            }
        }
    }
}
