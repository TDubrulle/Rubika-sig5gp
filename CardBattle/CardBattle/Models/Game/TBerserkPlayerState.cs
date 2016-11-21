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
            //Cannot turn to another state since we are berserk.
            return this;
        }

        public Card chooseCard(TPlayer tp)
        {
            return tp.getHighestCard();
        }
    }
}
