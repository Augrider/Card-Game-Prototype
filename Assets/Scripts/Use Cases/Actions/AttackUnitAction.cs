using System.Collections;
using Game.Entities.Units;
using Game.Entities.Tables;
using Game.Selection;
using UnityEngine;
using Game.Plugins.Coroutines;
using Game.Plugins.ObjectPool;
using System.Linq;

namespace Game.Actions
{
    public class AttackUnitAction : Action
    {
        private IUnit _attacker;
        private IUnit _target;

        public static ITargetData TargetData { get; set; }


        public AttackUnitAction(IUnit attacker)
        {
            _attacker = attacker;
        }


        public override void OnStackEnter()
        {
            //Start selecting unit and subscribe to selection events

            //Spot selected -> Start process:
            //Attack
            SelectTargetUnitState.TargetSelected += OnTargetSelected;
            SelectionState.Cancelled += Cancel;

            SelectionState.StateControl.GoToSelectTargetUnit(_attacker.UnitMovements.Position, GetSelectableUnits());

            _attacker.UnitHighlight.ToggleLift(true);
        }

        public override void OnStackExit()
        {
            //Attack finished -> Unsubscribe
            _attacker.State.ReadyToAttack = false;

            SelectTargetUnitState.TargetSelected -= OnTargetSelected;
            SelectionState.Cancelled -= Cancel;

            base.OnStackExit();
        }

        public override void OnCancelled()
        {
            //Attack cancelled -> Unsubscribe and stop selection
            SelectTargetUnitState.TargetSelected -= OnTargetSelected;
            SelectionState.Cancelled -= Cancel;

            SelectionState.StateControl.StopSelection();

            _attacker.UnitHighlight.ToggleLift(false);

            base.OnCancelled();
        }



        private IUnit[] GetSelectableUnits()
        {
            var targetTable = Table.GetTableSide(_attacker.Owner, TargetData.Alignment);
            var selectable = TargetData.GetAllowedTargets(targetTable);
            return selectable.Where(t => t != _attacker).ToArray();
        }


        private void OnTargetSelected(IUnit unit)
        {
            //Target selected -> Start process
            Debug.Log($"Unit {unit} selected, attacking");

            SelectTargetUnitState.TargetSelected -= OnTargetSelected;
            SelectionState.StateControl.StopSelection();

            PerformAttack(unit);
        }

        private void Cancel()
        {
            Debug.Log($"Cancelled");
            ActionStack.CancelCurrent();
        }


        private void PerformAttack(IUnit target)
        {
            _target = target;

            _attacker.UnitAnimations.AttackPerformed += OnAttackPerformed;
            _attacker.UnitAnimations.PlayAttack(target.UnitMovements.Position);
        }

        private void OnAttackPerformed()
        {
            _target.State.UnitHealth -= _attacker.State.UnitAttack;
            _attacker.State.UnitHealth -= _target.State.UnitAttack;

            _attacker.UnitHighlight.ToggleLift(false);

            Debug.Log($"Damage, attacker {_attacker.State.UnitHealth}, target {_target.State.UnitHealth}");

            _attacker.UnitAnimations.AttackPerformed -= OnAttackPerformed;

            StartCleanup();
        }
    }
}