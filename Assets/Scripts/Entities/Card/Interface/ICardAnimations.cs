using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities.Cards
{
    //Handles card animations
    public interface ICardAnimations
    {
        bool IsIdle { get; }

        void OnCardPlayed();
    }
}