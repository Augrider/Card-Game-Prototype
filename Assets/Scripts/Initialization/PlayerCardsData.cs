using System.Collections;
using System.Collections.Generic;
using Game.Components.Card;
using Game.Entities.Cards;
using UnityEngine;

namespace Game.Initialization
{
    [CreateAssetMenu(menuName = "Card Deck")]
    public class PlayerCardsData : ScriptableObject
    {
        [SerializeField] private string _heroName;
        [SerializeField] private Sprite _heroPortrait;

        [SerializeField] private CardProvider[] _cards;

        public CardVisuals PlayerVisuals { get => new CardVisuals(_heroName, _heroPortrait, string.Empty); }
        public IEnumerable<ICardProvider> Cards => _cards;
    }
}