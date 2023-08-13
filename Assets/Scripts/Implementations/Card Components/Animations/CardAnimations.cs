using System.Collections;
using System.Collections.Generic;
using Developed.TweenSystem;
using Developed.TweenSystem.Tweens;
using UnityEngine;

namespace Game.Components.Card
{
    public class CardAnimations : BaseCardAnimations
    {
        public override bool IsIdle => !Card.CardMovements.IsMoving;

        //Animations for card played, etc


        public override void OnCardPlayed()
        {
            Debug.LogWarning("Card disappear played!");
            //Play dissappear animation, put into graveyard
        }
    }
}