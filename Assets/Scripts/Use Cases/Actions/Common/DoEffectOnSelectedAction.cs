using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Actions
{
    /// <summary>
    /// Apply provided function on provided targets
    /// </summary>
    public class DoEffectOnSelectedAction : TargetableAction
    {
        private System.Action<IUnit> _unitEffect;
        protected override Vector3 LineStart => Vector3.zero;


        public DoEffectOnSelectedAction(Player player, ITargetData target, System.Action<IUnit> unitEffect, bool selectInsideAction) :
            base(player, target, selectInsideAction)
        {
            _unitEffect = unitEffect;
        }


        protected override void AfterTargetsSelected()
        {
            foreach (var target in CurrentTargets)
                _unitEffect.Invoke(target);

            ActionStack.FinishCurrent();
        }
    }
}