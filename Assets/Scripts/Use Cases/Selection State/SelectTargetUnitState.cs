using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Entities.Tables;
using Game.Entities.Units;
using Game.Plugins.Input;
using UnityEngine;

namespace Game.Selection
{
    public class SelectTargetUnitState : SelectionState
    {
        public static event Action<IUnit> TargetSelected;

        public IUnit[] SelectableUnits { get; set; } = new IUnit[0];


        public override void OnStateEnter()
        {
            //Highlight targets
            //Start drawing line (arrow) to pointer
            //Enable tooltips?

            InputLocator.Service.UnitSelected += OnUnitSelected;

            ToggleHighlight(true, SelectableUnits);

            Debug.Log($"Selecting, targets amount {SelectableUnits.Count()}");
        }

        public override void OnStateExit()
        {
            InputLocator.Service.UnitSelected -= OnUnitSelected;

            ToggleHighlight(false, SelectableUnits);
        }



        private void OnUnitSelected(IUnit unit)
        {
            //if selectable - send target selected
            if (!SelectableUnits.Contains(unit))
                return;

            TargetSelected?.Invoke(unit);
        }

        private void ToggleHighlight(bool value, IEnumerable<IUnit> selectables)
        {
            foreach (var unit in selectables)
                unit.UnitHighlight.StrokeEnabled = value;
        }
    }
}