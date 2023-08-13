using System.Collections;
using System.Collections.Generic;
using Game.Components.Card;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Plugins.Input
{
    public class CardPointerEvents : CardComponent, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            // Debug.Log("Click!");
            if (InputLocator.Service.InputEnabled)
                InputLocator.Service.InvokeCardSelectedEvent(Card);
        }
    }
}