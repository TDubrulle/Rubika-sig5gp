using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardBattle.Models.Game
{
    interface TPlayerState
    {
        Card chooseCard(TPlayer tp);
        TPlayerState changeState(TPlayer tp);
    }
}
