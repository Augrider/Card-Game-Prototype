using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities.Units
{
    public interface IUnitAnimations
    {
        bool IsIdle { get; }

        event Action AttackPerformed;

        void PlayAttack(Vector3 position);

        void OnDamageReceived();
        void OnDeath();
        void OnPlaced();
    }
}