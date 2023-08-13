using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Components.Unit
{
    public class UnitState : IUnitState
    {
        private Unit _unit;

        private bool _readyToAttack;

        private int _unitDamage;
        private int _unitHealth;
        private int _maxHealth;

        private bool _taunt;
        private bool _charge;

        public bool ReadyToAttack { get => _readyToAttack; set => SetReadyStatus(value); }

        public UnitType UnitType { get; private set; }

        public int UnitAttack { get => _unitDamage; set => SetDamage(value); }
        public int UnitHealth { get => _unitHealth; set => SetHealth(value); }
        public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }

        public bool Taunt { get => _taunt; set => SetTaunt(value); }
        public bool Charge { get => _charge; set => SetCharge(value); }

        public UnitState(Unit unit)
        {
            _unit = unit;
        }


        public void ReplaceFrom(UnitStats stats)
        {
            UnitType = stats.UnitType;

            _unitDamage = stats.UnitAttack;
            _unitHealth = stats.UnitMaxHealth;
            _maxHealth = stats.UnitMaxHealth;

            _taunt = stats.Taunt;
            _charge = stats.Charge;

            _unit.StateChangedNotify();
        }



        private void SetReadyStatus(bool value)
        {
            _readyToAttack = value;
            _unit.StateChangedNotify();
        }


        private void SetDamage(int value)
        {
            _unitDamage = value;
            _unit.StateChangedNotify();
        }

        private void SetHealth(int value)
        {
            _unitHealth = value;
            _unit.StateChangedNotify();
        }

        private void SetMaxHealth(int value)
        {
            _maxHealth = value;
            _unitHealth = Mathf.Clamp(_unitHealth, 0, _maxHealth);

            _unit.StateChangedNotify();
        }


        private void SetTaunt(bool value)
        {
            _taunt = value;
            _unit.StateChangedNotify();
        }

        private void SetCharge(bool value)
        {
            _charge = value;
            _unit.StateChangedNotify();
        }
    }
}