using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Entities.Cards;
using System;

namespace Game.Selection
{
    public class SelectCardsState : SelectionState
    {
        public static System.Action<IEnumerable<ICard>> SelectionConfirmed;

        private ISelectCardsScreen _selectCardsScreen;
        public IEnumerable<ICard> SelectableCards { get; set; }


        public SelectCardsState(ISelectCardsScreen selectCardsScreen)
        {
            _selectCardsScreen = selectCardsScreen;
        }


        public override void OnStateEnter()
        {
            _selectCardsScreen.SetActivePlayer(CurrentTable.Player);

            _selectCardsScreen.Enabled = true;
            _selectCardsScreen.SetCards(SelectableCards.ToArray());

            _selectCardsScreen.SelectionConfirmed += OnSelectionEnded;
            Cancelled += OnSelectionEnded;
        }

        public override void OnStateExit()
        {
            _selectCardsScreen.Enabled = false;

            _selectCardsScreen.SelectionConfirmed -= OnSelectionEnded;
            Cancelled -= OnSelectionEnded;

            SelectableCards = null;
        }



        private void OnSelectionEnded()
        {
            SelectionConfirmed?.Invoke(_selectCardsScreen.SelectedCards);
        }
    }
}