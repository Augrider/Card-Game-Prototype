using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Game.Entities.Tables;
using Game.Entities.Units;
using Game.Plugins.Coroutines;
using Game.Plugins.ObjectPool;
using Game.Selection;
using UnityEngine;
using Game.Entities;

namespace Game.Actions
{
    public class PlaceUnitAction : Action
    {
        //Mostly use references instead of structs

        //Place unit from IUnitCard on target spot
        //Do normal procedures for placement of unit copy
        //Play Battlecry effect action, play On table effect action
        //Add support for IUnitData instead of card itself for ability to spawn units from effects
        private IUnitSpawnData _spawnData;
        private ITableSide _table;

        private Queue<IAction> _actionQueue = new Queue<IAction>();


        //Remove position, it will be selected in execution
        public PlaceUnitAction(IUnitSpawnData spawnData, ITableSide table)
        {
            _spawnData = spawnData;
            _table = table;
        }


        public override void OnStackEnter()
        {
            //Start selecting spot and subscribe to selection events
            SelectUnitSpotState.UnitSpotSelected += OnPlaceSelected;
            SelectionState.Cancelled += Cancel;

            SelectionState.StateControl.GoToSelectUnitSpot(_table.HeroUnitPlace.Position);
        }

        public override void OnStackExit()
        {
            //Unit successfully placed -> Unsubscribe
            SelectUnitSpotState.UnitSpotSelected -= OnPlaceSelected;

            base.OnStackExit();
        }

        public override void OnCancelled()
        {
            //Unit failed to be placed -> Unsubscribe, stop selection
            SelectUnitSpotState.UnitSpotSelected -= OnPlaceSelected;
            SelectionState.Cancelled -= Cancel;

            SelectionState.StateControl.StopSelection();

            base.OnCancelled();
        }



        private void OnPlaceSelected(IUnitPlace place)
        {
            //Spot selected -> Start process
            if (place.Occupied || !_table.UnitPlaces.Contains(place))
                return;

            Debug.Log($"Place {place} selected");

            SelectUnitSpotState.UnitSpotSelected -= OnPlaceSelected;
            SelectionState.StateControl.StopSelection();

            CoroutinesLocator.Service.StartCoroutine(UnitPlacementProcess(_table, place));
        }

        private void Cancel()
        {
            Debug.Log($"Cancelled");

            SelectionState.StateControl.StopSelection();
            ActionStack.CancelCurrent();
        }


        private IEnumerator UnitPlacementProcess(ITableSide tableSide, IUnitPlace place)
        {
            var unit = ObjectPoolLocator.Service.GetNewUnit(tableSide.Player, _spawnData, place.Position, place.Rotation);
            tableSide.PlaceUnit(unit, place);

            yield return new WaitWhile(() => unit.UnitAnimations.IsIdle);

            if (unit.State.Charge)
                unit.State.ReadyToAttack = true;

            foreach (var effect in unit.UnitPlacedEffects)
                _actionQueue.Enqueue(GetOnPlacedAction(unit, effect));

            //Resolve queue on stack
            PutNextActionOnStack();
        }

        private void PutNextActionOnStack()
        {
            //If actions finished - complete current task
            if (_actionQueue.Count <= 0)
            {
                StartApplyingTableEffects();
                return;
            }

            var action = _actionQueue.Dequeue();
            action.Cancelled += PutNextActionOnStack;
            action.Finished += PutNextActionOnStack;

            ActionStack.PutOnStack(action);
        }


        private static IAction GetOnPlacedAction(IUnit unit, IEffectProvider actionProvider)
        {
            var action = actionProvider.GetAction(unit.Owner);

            if (action is ITargetableAction targetableAction)
            {
                var targetTable = Table.GetTableSide(unit.Owner, targetableAction.TargetData.Alignment);
                targetableAction.CurrentTargets = targetableAction.TargetData.GetAllowedTargets(targetTable).ToArray();
            }

            return action;
        }
    }
}