using System.Collections;
using Game.Entities.Cards;
using Game.Entities.Tables;
using Game.Plugins.Coroutines;
using Game.Plugins.Input;
using UnityEngine;

namespace Game.Selection
{
    public class SelectUnitSpotState : SelectionState
    {
        public static event System.Action<IUnitPlace> UnitSpotSelected;


        public override void OnStateEnter()
        {
            //Disable tooltips (unit and card increase)

            Debug.Log("Selecting Spot");

            InputLocator.Service.UnitPlaceSelected += OnUnitSpotSelected;
            InputLocator.Service.Cancelled += Cancel;
        }

        public override void OnStateExit()
        {
            InputLocator.Service.UnitPlaceSelected -= OnUnitSpotSelected;
            InputLocator.Service.Cancelled -= Cancel;
        }



        private void OnUnitSpotSelected(IUnitPlace place)
        {
            UnitSpotSelected?.Invoke(place);
        }
    }
}