using Game.Actions;
using Game.Entities;
using Game.Entities.Tables;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Components.Effects
{
    [CreateAssetMenu(menuName = "Effects/Heal", order = 0)]
    public class HealUnitEffectProvider : TargetableUnitEffectProvider
    {
        [SerializeField] private int _amount;


        public override ITargetableEffectProvider GetTargetableEffect()
        {
            return new HealUnitEffect(GetTargetData(), _amount, _selectInsideAction);
        }
    }



    public class HealUnitEffect : BaseUnitEffect
    {
        [SerializeField] private int _amount;


        public HealUnitEffect(ITargetData targetData, int amount, bool selectInsideAction) : base(targetData, selectInsideAction)
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
        /// Add health to target
        /// </summary>
        private void ApplyEffect(IUnit target)
        {
            target.State.UnitHealth = Mathf.Clamp(target.State.UnitHealth + _amount, 0, target.State.MaxHealth);
        }

        /// <summary>
        /// Remove health
        /// </summary>
        private void ReverseEffect(IUnit target)
        {
            target.State.UnitHealth = Mathf.Clamp(target.State.UnitHealth - _amount, 0, target.State.MaxHealth);
        }
    }
}