using System.Collections;
using System.Collections.Generic;
using Game.Entities;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Components.Card
{
    public class UnitCard : Card, IUnitCard
    {
        public UnitStats UnitStats { get; set; }

        public IEffectProvider[] UnitPlacedEffects { get; set; }
        public ITargetableEffectProvider[] UnitOnTableEffects { get; set; }
    }
}