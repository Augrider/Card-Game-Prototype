using System.Collections;
using System.Collections.Generic;
using Game.Components.Targets;
using Game.Entities;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Components.Effects
{
    public abstract class BaseUnitEffectProvider : ScriptableObject
    {
        public abstract IEffectProvider GetEffect();
    }



    public abstract class TargetableUnitEffectProvider : BaseUnitEffectProvider
    {
        [SerializeField] protected TargetAlignment _alignment;
        [SerializeField] protected TargetType _targetType;

        [SerializeField] protected bool _ignoreTaunt;
        [SerializeField] protected UnitType _unitType;

        [SerializeField] protected bool _selectInsideAction;


        public sealed override IEffectProvider GetEffect() => GetTargetableEffect();
        public abstract ITargetableEffectProvider GetTargetableEffect();

        protected virtual ITargetData GetTargetData()
        {
            return new DefaultTarget(_alignment, _targetType, _unitType, _ignoreTaunt);
        }
    }
}