using System;
using System.Collections;
using System.Collections.Generic;
using Game.Actions;
using Game.Entities.Cards;
using Game.Entities.Tables;
using Game.GameState;
using Game.Plugins.Coroutines;
using Game.Plugins.Input;
using UnityEngine;

namespace Game.Selection
{
    //Do this for cards that do not require selecting anything specific
    public class DragCardState : SelectionState
    {
        public static event System.Action<ICard, Vector3, TableArea> DragFinished;

        public ICard CurrentCard { get; set; }


        //TODO: Add better drag finish conditions (maybe add on drag started/finished events in pointer events?)
        public override void OnStateEnter()
        {
            //Disable tooltips (unit and card increase)
            InputLocator.Service.Cancelled += OnDragFinished;
            SelectionVisuals.StartCardFollowingPointer(CurrentCard);
        }

        public override void OnStateExit()
        {
            InputLocator.Service.Cancelled -= OnDragFinished;
            SelectionVisuals.StopCardFollowing();
        }



        private void OnDragFinished()
        {
            var position = InputLocator.Service.PointerPosition;
            DragFinished?.Invoke(CurrentCard, position, CurrentTable.GetClosestArea(position));
        }


        // private IEnumerator DragProcess()
        // {
        //     //Move card after pointer
        //     while (true)
        //     {
        //         CurrentCard.CardMovements.MoveTo(InputLocator.Service.PointerPosition);
        //         yield return null;
        //     }
        // }
    }
}