using System.Collections;
using System.Collections.Generic;
using Game.Entities.Cards;
using UnityEngine;

namespace Game.Components.Card
{
    public abstract class CardComponent : MonoBehaviour
    {
        [SerializeField] private Card _card;
        protected ICard Card => _card;
    }
}