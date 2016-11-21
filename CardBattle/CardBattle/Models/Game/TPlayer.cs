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
        #region table
        public List<int> playersScore = new List<int>();
        public int position = -1;
        public int playerCount = -1;
        public int foldsLeft = 0;
        #endregion
        private TPlayerState currentState;

        #region IPlayer implementation
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
            foldsLeft = cards.Count();
            currentState = new TThoughtfulPlayerState();
        }

        public void Initialize(int playerCount, int position)
        {
            resetRemainingGameCards();
            this.position = position;
            this.playerCount = playerCount;
            initPlayersScores(playerCount);
        }

        public Card PlayCard()
        {
            if(hand.Count == 0) { throw new IndexOutOfRangeException("Cannot play cards without any in hand!"); }
            currentState = currentState.changeState(this);
            Card t = currentState.chooseCard(this);
            hand.Remove(t);
            foldsLeft--;
            return t;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            playersScore[result.Winner]++;
            removeGameCards(result.CardsPlayed);
        }
        #endregion

        #region players'scores
        public void initPlayersScores(int nPlayers)
        {

            this.playersScore = new List<int>(playerCount);
            for (int i = 0; i < nPlayers; ++i)
            {
                playersScore.Add(0);
            }
        }

        public int getHighestEnemyScore()
        {
            int max = 0;
            for(int i = 0; i < playersScore.Count; ++i)
            {
                if(max < playersScore[i] && i != this.position)
                {
                    max = playersScore[i];
                }
            }
            return max;
        }
        #endregion

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
                float winningProbability = (float)higherCardsNumber / (float)remainingGameCards.Count;
                //We don't bother trying if winning chance is less than 10%.
                if (winningProbability < 0.10f) return false;
                //Else we choose randomly, based on our probability to win.
                float decision = 1f - (float)_rand.NextDouble();
                float decisionBaseValue = decision;

                for (int i = 1; i < playerCount; ++i)
                {
                    //We reduce the chance this decision will be taken based on the number of adversaries. 
                    decision = decision * decisionBaseValue;
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
