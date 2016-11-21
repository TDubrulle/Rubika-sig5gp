using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardBattle.Models;

namespace CardBattle.Player
{
    public class MinValuePlayer : IPlayer
    {
        public string Author
        {
            get
            {
                return "JN";
            }
        }

        public string Name
        {
            get
            {
                return "MinValue";
            }
        }

        private List<Card> _hand;
        public void Deal(IEnumerable<Card> cards)
        {
            _hand = cards.ToList();
        }

        public void Initialize(int playerCount, int position)
        {
        }

        public Card PlayCard()
        {
            var result = _hand.Min();
            _hand.Remove(result);
            return result;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
        }
    }
}
