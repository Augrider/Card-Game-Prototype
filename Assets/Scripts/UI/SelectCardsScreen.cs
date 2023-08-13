using System.Linq;
using Game.Components.Table;
using Game.Entities.Cards;
using Game.Plugins.Input;
using Game.Selection;
using UnityEngine;

namespace Game.UI
{
    public sealed class SelectCardsScreen : BaseCardsSelectionScreen
    {
        //Canvas just turns for different players
        [SerializeField] private Canvas _screenCanvas;
        [SerializeField] private Transform _pivot;

        [SerializeField] private CardHand _cardHand;
        [SerializeField] private Vector3 _cardScale;

        public override bool Enabled { get => _screenCanvas.enabled; set => ToggleScreen(value); }
        public override int MaxSelected { get; set; }


        private void OnEnable()
        {
            InputLocator.Service.CardSelected += OnCardSelected;
        }

        private void OnDisable()
        {
            InputLocator.Service.CardSelected -= OnCardSelected;
        }


        public override void SetActivePlayer(Player player)
        {
            var pivotEuler = Vector3.zero;
            pivotEuler.z = player == Player.One ? 0 : 180;

            _pivot.eulerAngles = pivotEuler;
        }

        public override void SetCards(params ICard[] cards)
        {
            foreach (var card in cards)
            {
                _cardHand.TryAddCard(card);
                card.CardMovements.Scale = _cardScale;
            }

            AllCards = cards;
        }



        private void ToggleScreen(bool value)
        {
            _screenCanvas.enabled = value;
            enabled = value;

            if (!value)
                CleanCards();
        }

        private void CleanCards()
        {
            foreach (var card in AllCards)
            {
                card.CardMovements.Scale = Vector3.one;
                card.CardHighlight.StrokeEnabled = false;
            }

            _cardHand.Clear();

            SelectedCards = new ICard[0];
            AllCards = new ICard[0];
        }


        private void OnCardSelected(ICard card)
        {
            if (!AllCards.Contains(card))
                return;

            Debug.Log("Pressed card!");
            card.CardHighlight.StrokeEnabled = !card.CardHighlight.StrokeEnabled;

            SelectedCards = AllCards.Where(t => t.CardHighlight.StrokeEnabled);
        }
    }
}