using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entities;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Components.Unit
{
    public class Unit : MonoBehaviour, IUnit
    {
        [SerializeField] private TweenMovementComponent _movementComponent;
        [SerializeField] private BaseUnitAnimations _unitAnimations;
        [SerializeField] private HighlightComponent _highlightComponent;

        public event Action StateChanged;

        public Player Owner { get; set; }

        public IUnitState State { get; set; }
        public IList<ITargetableEffectProvider> CurrentBuffs { get; } = new List<ITargetableEffectProvider>();

        public IEffectProvider[] UnitPlacedEffects { get; set; } = new IEffectProvider[0];
        public ITargetableEffectProvider[] UnitOnTableEffects { get; set; } = new ITargetableEffectProvider[0];

        public IMovement UnitMovements => _movementComponent;
        public IUnitAnimations UnitAnimations => _unitAnimations;
        public IHighlight UnitHighlight => _highlightComponent;


        private void Awake()
        {
            State = new UnitState(this);
        }

        public void StateChangedNotify() => StateChanged?.Invoke();
    }
}