using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities.Units
{
    public interface IUnitState
    {
        bool ReadyToAttack { get; set; }

        UnitType UnitType { get; }

        int UnitAttack { get; set; }
        int UnitHealth { get; set; }
        int MaxHealth { get; set; }

        bool Taunt { get; set; }
        bool Charge { get; set; }

        void ReplaceFrom(UnitStats stats);
    }
}