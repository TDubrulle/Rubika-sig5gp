﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardBattle.Models.Game
{
    class TThoughtfulPlayerState : TPlayerState
    {
        public TPlayerState changeState(TPlayer tp)
        {
            //We switch to berserk mode if one player is close to reach victory. 
            /*if (tp.getHighestEnemyScore() > tp.foldsLeft + 1)
            {
                return new TBerserkPlayerState();
            }*/
            return this;
        }

        public Card chooseCard(TPlayer tp)
        {
            //We choose the highest card if we have good chances to win with it.
            //Otherwise we choose the lowest card.
            Card highestCard = tp.getHighestCard();
            if (tp.canWinWith(highestCard))
            {
                return highestCard;
            } else
            {
                return tp.getLowestCard();
            }
        }
    }
}
