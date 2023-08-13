using System.Collections.Generic;
using Developed.Extentions;
using Game.Entities.Cards;
using Game.Entities.Tables;
using UnityEngine;

namespace Game.Components.Table
{
    public sealed class CardDeck : MonoBehaviour, ICardDeck
    {
        [SerializeField] private Vector3 _cardOffset;

        private Stack<ICard> _cards = new Stack<ICard>();

        public int CardAmount => _cards.Count;


        public void AddCards(params ICard[] cards)
        {
            foreach (var card in cards)
            {
                _cards.Push(card);
                card.CardHighlight.ToggleHide(true);
            }

            PlaceCards();
        }

        public bool TryGetCard(out ICard card)
        {
            card = null;

            if (CardAmount <= 0)
                return false;

            card = _cards.Pop();
            PlaceCards();

            return true;
        }


        public void Shuffle()
        {
            _cards = _cards.Shuffle();
        }



        private void PlaceCards()
        {
            var cards = _cards.ToArray();
            for (int i = 0; i < CardAmount; i++)
                cards[i].CardMovements.MoveTo(transform.position + _cardOffset * (CardAmount - i - 1));
        }
    }
}