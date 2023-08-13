using System.Collections;
using System.Collections.Generic;
using Game.Entities.Cards;
using UnityEngine;

namespace Game.Components.Card
{
    // [CreateAssetMenu(menuName = "Cards/Basic")]
    public abstract class CardProvider : ScriptableObject, ICardProvider
    {
        [SerializeField] private int _manaCost;

        [SerializeField] private string _cardName;
        [SerializeField] private Sprite _cardPortrait;
        [SerializeField] private string _description;
        //TODO: Card effect for spells, constructed somehow

        public abstract bool IsUnitCard { get; }


        public void SetCardParameters(GameObject card, Player owner)
        {
            var cardObject = card.GetComponent<Card>();
            var cardVisualComponent = card.GetComponent<CardVisualComponent>();

            cardObject.VisualStats = new CardVisuals(_cardName, _cardPortrait, _description);
            cardVisualComponent.SetCardVisuals(cardObject.VisualStats);

            cardObject.ManaCost = _manaCost;
            SetCardSpecificParameters(cardObject);

            cardObject.StateChangedNotify();
        }


        protected abstract void SetCardSpecificParameters(Card cardObject);
    }
}