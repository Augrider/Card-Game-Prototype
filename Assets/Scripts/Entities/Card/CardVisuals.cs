using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities.Cards
{
    [System.Serializable]
    public struct CardVisuals
    {
        public string CardName;
        public Sprite CardPortrait;
        public string Description;

        public CardVisuals(string cardName, Sprite cardPortrait, string description)
        {
            CardName = cardName;
            CardPortrait = cardPortrait;
            Description = description;
        }
    }
}