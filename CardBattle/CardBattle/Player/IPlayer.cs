using CardBattle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardBattle.Player
{
    public interface IPlayer
    {
        string Name { get;}
        string Author { get; }

        void Initialize(int playerCount, int position);
        void Deal(IEnumerable<Card> cards);
        Card PlayCard();
        void ReceiveFoldResult(FoldResult result);
    }
}
