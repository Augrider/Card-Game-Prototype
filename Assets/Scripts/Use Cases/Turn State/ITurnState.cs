using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Turn
{
    public interface ITurnState
    {
        void OnStateEnter();
        void OnStateExit();
    }
}