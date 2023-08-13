using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities.Units
{
    public interface IUnit
    {
        event System.Action StateChanged;

        Player Owner { get; }

        IUnitState State { get; }
        IList<ITargetableEffectProvider> CurrentBuffs { get; }

        IEffectProvider[] UnitPlacedEffects { get; }
        ITargetableEffectProvider[] UnitOnTableEffects { get; }
        // IEffectProvider UnitDeadEffect { get; }

        IMovement UnitMovements { get; }
        IUnitAnimations UnitAnimations { get; }
        IHighlight UnitHighlight { get; }

        //Unit have hp and attack. Also, unit type (if any)
        //Units should have state that shows if it can do attack right now by current player (bool)

        //Effect types: Battlecry (on unit placed), when placed (just add effect when alive? or add special effect on table/character?), Charge (attack right from the start), Taunt (if exists, only units with it can be attacked)
        //Main character can also be unit, just without a lot of things
        //Units share attack behavior (can be put outside), but their on attack effects are not

        //Additional info: portrait

        //Effect descriptions are not required, only their presence on unit
        //They are shown by unit itself


        //Based on all of this, units should not just have static effects, but can apply temporary effects on their own
    }
}