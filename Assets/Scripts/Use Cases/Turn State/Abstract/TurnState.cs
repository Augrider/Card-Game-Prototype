using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Turn
{
    public abstract class TurnState : ITurnState
    {
        protected const float TIME_BETWEEN_ACTIONS = 0.5f;

        public static ITurnStateControl TurnStateControl { get; set; }
        protected static YieldInstruction WaitBetweenActions { get; } = new WaitForSeconds(TIME_BETWEEN_ACTIONS);


        public abstract void OnStateEnter();
        public abstract void OnStateExit();
    }
}