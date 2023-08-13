using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Entities;
using Game.Entities.Tables;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Actions
{
    public class ApplyTableEffectsAction : Action
    {
        private Queue<IAction> _actionQueue = new Queue<IAction>();


        public override void OnStackEnter()
        {
            //Enqueue all table effects from all units
            EnqueueFromTable(Table.GetTableSide(Player.One));
            EnqueueFromTable(Table.GetTableSide(Player.Two));

            PutNextActionOnStack();
        }



        private void EnqueueFromTable(ITableSide tableSide)
        {
            foreach (var unit in tableSide.Units)
            {
                //For each effect check if has targets and enqueue
                foreach (var tableEffect in unit.UnitOnTableEffects)
                    if (TryGetOnTableAction(tableSide.Player, tableEffect, out var action))
                        _actionQueue.Enqueue(action);
            }
        }


        private void PutNextActionOnStack()
        {
            //If actions finished - complete current task
            if (_actionQueue.Count <= 0)
            {
                StartCleanup();
                return;
            }

            var action = _actionQueue.Dequeue();

            action.Cancelled += PutNextActionOnStack;
            action.Finished += PutNextActionOnStack;

            ActionStack.PutOnStack(action);
        }


        private bool TryGetOnTableAction(Player player, ITargetableEffectProvider tableEffect, out ITargetableAction action)
        {
            action = tableEffect.GetAction(player);
            var targetTable = Table.GetTableSide(player, action.TargetData.Alignment);

            var targets = action.TargetData.GetAllowedTargets(targetTable);
            var finalTargets = targets.Where(t => !t.CurrentBuffs.Contains(tableEffect)).ToArray();

            if (finalTargets.Length <= 0)
                return false;

            foreach (var target in finalTargets)
                target.CurrentBuffs.Add(tableEffect);

            action.CurrentTargets = finalTargets.ToArray();

            return true;
        }
    }
}