using Game.Actions;
using Game.Entities;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Components.Effects
{
    [CreateAssetMenu(menuName = "Effects/Add Attack", order = 0)]
    public class AddAttackUnitEffectProvider : TargetableUnitEffectProvider
    {
        [SerializeField] private int _amount;


        public override ITargetableEffectProvider GetTargetableEffect()
        {
            return new AddAttackUnitEffect(GetTargetData(), _amount, _selectInsideAction);
        }
    }




    public class AddAttackUnitEffect : BaseUnitEffect
    {
        [SerializeField] private int _amount;


        public AddAttackUnitEffect(ITargetData targetData, int amount, bool selectInsideAction) : base(targetData, selectInsideAction)
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
        /// Add attack to targets
        /// </summary>
        private void ApplyEffect(IUnit target)
        {
            target.State.UnitAttack = Mathf.Clamp(target.State.UnitAttack + _amount, 0, 99);
        }

        /// <summary>
        /// Remove attack
        /// </summary>
        private void ReverseEffect(IUnit target)
        {
            target.State.UnitAttack = Mathf.Clamp(target.State.UnitAttack - _amount, 0, 99);
        }
    }
}