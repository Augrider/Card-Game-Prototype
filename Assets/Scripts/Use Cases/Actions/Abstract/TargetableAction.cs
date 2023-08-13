using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Entities.Tables;
using Game.Entities.Units;
using Game.Selection;
using UnityEngine;

namespace Game.Actions
{
    public abstract class TargetableAction : Action, ITargetableAction
    {
        protected Player Player { get; private set; }

        protected abstract Vector3 LineStart { get; }

        public ITargetData TargetData { get; private set; }
        public IUnit[] CurrentTargets { get; set; }

        public bool SelectInsideAction { get; private set; }


        public TargetableAction(Player player, ITargetData target, bool selectInsideAction = false)
        {
            Player = player;
            TargetData = target;

            SelectInsideAction = selectInsideAction;
        }

        //Allow to select targets if selection is enabled


        public sealed override void OnStackEnter()
        {
            if (SelectInsideAction)
            {
                StartSelection();
                return;
            }

            AfterTargetsSelected();
        }


        protected abstract void AfterTargetsSelected();


        protected IEnumerable<IUnit> GetSelectableUnits()
        {
            var targetTable = Table.GetTableSide(Player, TargetData.Alignment);
            return TargetData.GetAllowedTargets(targetTable);
        }



        private void StartSelection()
        {
            SelectTargetUnitState.TargetSelected += OnTargetSelected;
            SelectionState.Cancelled += Cancel;

            SelectionState.StateControl.GoToSelectTargetUnit(LineStart, GetSelectableUnits().ToArray());
        }


        private void OnTargetSelected(IUnit unit)
        {
            //Target selected -> Start process
            Debug.Log($"Unit {unit} selected");

            SelectTargetUnitState.TargetSelected -= OnTargetSelected;
            SelectionState.Cancelled -= Cancel;

            SelectionState.StateControl.StopSelection();

            CurrentTargets = new IUnit[] { unit };

            AfterTargetsSelected();
        }

        private void Cancel()
        {
            Debug.Log($"Cancelled");

            SelectTargetUnitState.TargetSelected -= OnTargetSelected;
            SelectionState.Cancelled -= Cancel;

            ActionStack.CancelCurrent();
        }
    }
}