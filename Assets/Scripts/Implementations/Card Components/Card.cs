using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entities;
using Game.Entities.Cards;
using UnityEngine;

namespace Game.Components.Card
{
    public class Card : MonoBehaviour, ICard
    {
        [SerializeField] private BaseCardAnimations _cardAnimations;
        [SerializeField] private TweenMovementComponent _movementComponent;
        [SerializeField] private HighlightComponent _highlightComponent;

        public event Action StateChanged;

        public int ManaCost { get; set; }
        public Player Owner { get; set; }

        public CardVisuals VisualStats { get; set; }
        public IEffectProvider CardEffect { get; set; }

        public IMovement CardMovements => _movementComponent;
        public ICardAnimations CardAnimations => _cardAnimations;

        public IHighlight CardHighlight => _highlightComponent;


        public void StateChangedNotify() => StateChanged?.Invoke();
    }
}