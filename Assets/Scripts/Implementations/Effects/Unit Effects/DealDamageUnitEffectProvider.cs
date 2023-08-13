using Game.Actions;
using Game.Entities;
using Game.Entities.Tables;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Components.Effects
{
    [CreateAssetMenu(menuName = "Effects/Deal Damage", order = 0)]
    public class DealDamageUnitEffectProvider : TargetableUnitEffectProvider
    {
        [SerializeField] private int _amount;


        public override ITargetableEffectProvider GetTargetableEffect()
        {
            return new DealDamageUnitEffect(GetTargetData(), _amount, _selectInsideAction);
        }
    }



    public class DealDamageUnitEffect : BaseUnitEffect
    {
        [SerializeField] private int _amount;


        public DealDamageUnitEffect(ITargetData targetData, int amount, bool selectInsideAction) : base(targetData, selectInsideAction)
        {
            _amount = amount;
        }


        public override ITargetableAction GetAction(Player player)
        {
            return new DoEffectOnSelectedAction(player, TargetData, ApplyEffect, SelectInsideAction);
        }

        public override ITargetableAction GetReverseAction(Player player)
        {
            return new DoEffectOnSelectedAction(player, TargetData, ReverseEffect, SelectInsideAction);
        }



        /// <summary>
        /// Remove health from targets
        /// </summary>
        private void ApplyEffect(IUnit target)
        {
            target.State.UnitHealth = Mathf.Clamp(target.State.UnitHealth - _amount, 0, target.State.MaxHealth);
        }

        /// <summary>
        /// Add health
        /// </summary>
        private void ReverseEffect(IUnit target)
        {
            target.State.UnitHealth = Mathf.Clamp(target.State.UnitHealth + _amount, 0, target.State.MaxHealth);
        }
    }
}