using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Turn
{
    public interface ITurnStateControl
    {
        void GoToSelectStartHand(Player player);
        void GoToNormalTurns();
        void Stop();
    }
}