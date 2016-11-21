using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardBattle.Models.Game
{
    class TBerserkPlayerState : TPlayerState
    {
        public TPlayerState changeState(TPlayer tp)
        {
            //Cannot return to normal once we are berserk.
            return this;
        }

        public Card chooseCard(TPlayer tp)
        {
            //We can't lose a hand; we choose the highest card.
            throw new NotImplementedException();
        }
    }
}
