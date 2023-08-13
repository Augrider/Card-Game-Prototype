using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Entities;
using Game.Entities.Units;
using Game.Plugins.Coroutines;
using Game.Plugins.ObjectPool;
using UnityEngine;

namespace Game.Actions
{
    public class CleanDeadUnitsAction : Action
    {
        private IEnumerable<IUnit> _allUnits;
        private Queue<IAction> _actionQueue = new Queue<IAction>();


        public override void OnStackEnter()
        {
            //For each dead unit remove its table effects from others
            //Clean and leave

            _allUnits = ObjectPoolLocator.Service.GetActiveUnits();
            EnqueueReverseEffects(_allUnits);

            PutNextActionOnStack();
        }


        private void PutNextActionOnStack()
        {
            //If actions finished - complete current task
            if (_actionQueue.Count <= 0)
            {
                CoroutinesLocator.Service.StartCoroutine(RemovalProcess());
                return;
            }

            var action = _actionQueue.Dequeue();
            action.Cancelled += PutNextActionOnStack;
            action.Finished += PutNextActionOnStack;

            ActionStack.PutOnStack(action);
        }


        private void EnqueueReverseEffects(IEnumerable<IUnit> units)
        {
            foreach (var unit in units)
            {
                if (unit.State.UnitHealth > 0)
                    continue;

                foreach (var effect in unit.UnitOnTableEffects)
                    _actionQueue.Enqueue(GetOnPlacedReverseAction(unit, effect, units));
            }
        }

        private IAction GetOnPlacedReverseAction(IUnit unit, ITargetableEffectProvider actionProvider, IEnumerable<IUnit> allUnits)
        {
            var action = actionProvider.GetReverseAction(unit.Owner);

            var finalTargets = allUnits.Where(t => t.CurrentBuffs.Contains(actionProvider)).ToArray();

            foreach (var target in finalTargets)
                target.CurrentBuffs.Remove(actionProvider);

            action.CurrentTargets = finalTargets;
            return action;
        }


        private IEnumerator RemovalProcess()
        {
            yield return null;
            yield return new WaitUntil(() => _allUnits.All(t => t.UnitAnimations.IsIdle));

            ObjectPoolLocator.Service.RemoveDeadUnits();
            ActionStack.FinishCurrent();
        }
    }
}