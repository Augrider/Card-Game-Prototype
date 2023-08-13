using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entities.Tables;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Components.Targets
{
    public class DefaultTarget : ITargetData
    {
        private bool _ignoreTaunt;
        private UnitType _unitType;
        private TargetType _targetType;


        public TargetAlignment Alignment { get; private set; }


        public DefaultTarget(TargetAlignment alignment, TargetType targetType = TargetType.Unit, UnitType unitType = UnitType.None, bool ignoreTaunt = false)
        {
            Alignment = alignment;

            _ignoreTaunt = ignoreTaunt;
            _unitType = unitType;
            _targetType = targetType;
        }


        public IEnumerable<IUnit> GetAllowedTargets(ITableSide tableSide)
        {
            if (_ignoreTaunt)
                return tableSide.GetTargetTypeUnits(_targetType).FilterUnitsByType(_unitType);

            return tableSide.GetTargetTypeUnits(_targetType).FilterUnitsByType(_unitType).FilterUnitsByTaunt();
        }
    }
}