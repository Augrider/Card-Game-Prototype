using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Components.Unit
{
    public abstract class BaseUnitAnimations : UnitComponent, IUnitAnimations
    {
        public event Action AttackPerformed;

        public abstract bool IsIdle { get; }

        public abstract void OnPlaced();
        public abstract void OnDamageReceived();
        public abstract void OnDeath();

        public abstract void PlayAttack(Vector3 position);


        protected void InvokeAttackPerformed() => AttackPerformed?.Invoke();
    }
}