using System.Collections;
using System.Collections.Generic;
using Game.Entities.Cards;
using Game.Entities.Tables;
using UnityEngine;

namespace Game.Components.Table
{
    public sealed class CardHand : MonoBehaviour, ICardHand
    {
        [SerializeField] private int _maxHandSize;
        [SerializeField] private Vector3 _cardOffset;
        [SerializeField] private bool _hide = false;

        private List<ICard> _cards = new List<ICard>();

        public IEnumerable<ICard> Cards => _cards;
        public int CardAmount => _cards.Count;
        public bool IsFull => CardAmount >= _maxHandSize;


        public bool TryAddCard(ICard card)
        {
            if (IsFull)
                return false;

            _cards.Add(card);
            card.CardHighlight.ToggleHide(_hide);

            PlaceCards();

            return true;
        }

        public bool TryTakeCard(ICard card)
        {
            if (!_cards.Contains(card))
                return false;

            _cards.Remove(card);
            PlaceCards();

            return true;
        }

        public void Clear()
        {
            _cards.Clear();
        }


        public void ToggleHide(bool value)
        {
            _hide = value;

            foreach (var card in _cards)
                card.CardHighlight.ToggleHide(_hide);
        }



        private void PlaceCards()
        {
            var start = transform.position - (CardAmount / 2f - 0.5f) * _cardOffset;

            for (int i = 0; i < CardAmount; i++)
            {
                _cards[i].CardMovements.MoveTo(start + i * _cardOffset);
                _cards[i].CardMovements.RotateTo(transform.rotation);
            }
        }
    }
}