using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components
{
    public static class CommonFunctions
    {
        public static string GetUnitTypeString(UnitType unitType)
        {
            switch (unitType)
            {
                default:
                    return string.Empty;

                case UnitType.Murlock:
                    return "Murlock";
            }
        }
    }
}