using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components.Unit
{
    public class UnitHealthAnimationsHandler : UnitComponent
    {
        private int _lastCheck;


        private void OnEnable()
        {
            Unit.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            Unit.StateChanged -= OnStateChanged;
        }



        private void OnStateChanged()
        {
            if (Unit.State.UnitHealth == _lastCheck)
                return;

            CompareHealth(Unit.State.UnitHealth, _lastCheck);
            _lastCheck = Unit.State.UnitHealth;
        }

        private void CompareHealth(int current, int previous)
        {
            if (current > previous)
            {
                if (previous == 0)
                    Unit.UnitAnimations.OnPlaced();
            }
            else
            {
                if (current == 0)
                    Unit.UnitAnimations.OnDeath();
                else
                    Unit.UnitAnimations.OnDamageReceived();
            }
        }
    }
}