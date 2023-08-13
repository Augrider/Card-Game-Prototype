using System.Collections;
using System.Collections.Generic;
using Game.Entities.Cards;
using UnityEngine;

namespace Game.Components.Card
{
    public abstract class BaseCardAnimations : CardComponent, ICardAnimations
    {
        public abstract bool IsIdle { get; }

        public abstract void OnCardPlayed();
    }
}