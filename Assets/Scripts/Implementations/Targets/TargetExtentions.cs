using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Entities.Tables;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Components.Targets
{
    public static class TargetExtentions
    {
        public static ITargetData GetDefaultTarget(this TargetAlignment alignment) => new DefaultTarget(alignment);

        public static IEnumerable<IUnit> GetTargetTypeUnits(this ITableSide tableSide, TargetType targetType)
        {
            var result = new List<IUnit>();

            switch (targetType)
            {
                case TargetType.All:
                    result.Add(tableSide.HeroUnit);
                    result.AddRange(tableSide.Units);
                    return result;

                case TargetType.Hero:
                    result.Add(tableSide.HeroUnit);
                    return result;

                default:
                case TargetType.Unit:
                    result.AddRange(tableSide.Units);
                    return result;
            }
        }

        public static IEnumerable<IUnit> FilterUnitsByType(this IEnumerable<IUnit> units, UnitType unitType)
        {
            if (unitType == UnitType.None)
                return units;

            return units.Where(t => t.State.UnitType == unitType);
        }

        public static IEnumerable<IUnit> FilterUnitsByTaunt(this IEnumerable<IUnit> units)
        {
            if (!units.Any(t => t.State.Taunt))
                return units;

            return units.Where(t => t.State.Taunt);
        }
    }
}