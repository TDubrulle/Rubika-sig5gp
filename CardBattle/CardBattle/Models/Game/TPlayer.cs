using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardBattle.Models.Game
{
    class TPlayer : IPlayer
    {
        private readonly Random _rand = new Random();

        //The game remaining's cards, excluding his own.
        private List<Card> remainingGameCards;
        private static readonly int _suitsCount = Enum.GetValues(typeof(Suit)).Length;
        private static readonly int _valuesCount = Enum.GetValues(typeof(Values)).Length;

        //Player's hand
        //TTODO Sort the hand to improve performances.
        private List<Card> hand;

        private const string name = "Barbas le fou";
        private const string author = "Thomas Dubrulle";
        private int position = -1;
        private int playerCount = -1;
        private TPlayerState currentState;

        public string Author
        {
            get {
                return author;
            }
        }

        public string Name
        {
            get {
                return name;
            }
        }

        public void Deal(IEnumerable<Card> cards)
        {
            hand = cards.ToList();
            removeGameCards(cards);
        }
        public void Initialize(int playerCount, int position)
        {
            resetRemainingGameCards();
            currentState = new TThoughtfulPlayerState();
            this.position = position;
            this.playerCount = playerCount;
        }

        public Card PlayCard()
        {
            if(hand.Count == 0) { throw new IndexOutOfRangeException("Cannot play cards without any in hand!"); }
            currentState = currentState.changeState(this);
            Card t = currentState.chooseCard(this);
            return t;
        }

        public void ReceiveFoldResult(FoldResult result)
        {

        }

        #region handCards
        public Card getHighestCard()
        {
            return hand[getHighestCardIndex()];
        }

        public Card getLowestCard()
        {
            return hand[getHighestCardIndex()];
        }

        public int getHighestCardIndex()
        {
            int idxCard = 0;
            for (int i = 1; i < hand.Count; ++i)
            {
                if (hand[idxCard].CompareTo(hand[i]) < 0)
                {
                    idxCard = i;
                }
            }
            return idxCard;
        }

        public int getLowestCardIndex()
        {
            int idxCard = 0;
            for (int i = 1; i < hand.Count; ++i)
            {
                if (hand[idxCard].CompareTo(hand[i]) > 0)
                {
                    idxCard = i;
                }
            }
            return idxCard;
        }
        #endregion

        /// <summary>
        /// Tells if the fold can be probably won with the given card.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool canWinWith(Card c)
        {
            //We get the number of cards that are higher than the given card.
            int higherCardsIdx = 0;
            int higherCardsNumber = 0;
            for(int i = 0; i < remainingGameCards.Count; ++i)
            {
                if (c.CompareTo(remainingGameCards[i]) < 0)
                {
                    higherCardsIdx = i;
                }
            }
            higherCardsNumber = (remainingGameCards.Count -1) - higherCardsIdx;
            
            //Case we have no higher cards.
            if (higherCardsNumber <= 0)
            {
                return true;
            } else
            {
                //Else we choose randomly, based on our probability to win.
                float winningProbability = (float)higherCardsNumber / (float)remainingGameCards.Count;
                float decision = 1f - (float)_rand.NextDouble();
                for(int i = 1; i < playerCount; ++i)
                {
                    decision = decision * (decision + (1f-decision) * 0.5f);
                }
                if (decision > winningProbability)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }

        #region gameCards
        /// <summary>
        /// Reset the remaining game cards for decision.
        /// </summary>
        private void resetRemainingGameCards()
        {
            var list = new List<Card>();
            for (var i = 0; i < _suitsCount; i++)
            {
                for (var j = 0; j < _valuesCount; j++)
                {
                    list.Add(new Card((Values)j, (Suit)i));
                }
            }
            remainingGameCards = list;
        }

        private void removeGameCards(IEnumerable<Card> cards)
        {
            foreach(Card c in cards) {
                remainingGameCards.Remove(c);
            }
        }
        #endregion
    }
}
