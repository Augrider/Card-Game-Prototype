using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Entities.Cards;
using Game.Entities.Tables;
using Game.Plugins.Coroutines;
using Game.Selection;
using UnityEngine;

namespace Game.Turn
{
    public class SelectStartHandState : TurnState
    {
        private const int START_HAND_AMOUNT = 4;

        private ITableSide _currentTable;
        private ICard[] _cards;

        public Player CurrentPlayer { get; set; }



        public override void OnStateEnter()
        {
            //Draw 4 cards into provided hand
            //Subscribe to card selected event
            _currentTable = Table.GetTableSide(CurrentPlayer);

            for (int i = 0; i < START_HAND_AMOUNT; i++)
            {
                TryPutCardToHandFromDeck(_currentTable);
            }

            _cards = _currentTable.Hand.Cards.ToArray();
            _currentTable.Hand.Clear();

            SelectCardsState.SelectionConfirmed += OnSelectionEnded;

            SelectionState.StateControl.SetCurrentTable(_currentTable);
            SelectionState.StateControl.GoToSelectCardsFrom(_cards);
        }

        public override void OnStateExit()
        {
            //Clean selection screen
            SelectCardsState.SelectionConfirmed -= OnSelectionEnded;

            _cards = null;
        }



        private void OnSelectionEnded(IEnumerable<ICard> selected)
        {
            //All selected cards will go to deck
            //Shuffle deck
            //Get cards until hand got enough

            CoroutinesLocator.Service.StartCoroutine(AssembleHandProcess(selected));
        }

        private IEnumerator AssembleHandProcess(IEnumerable<ICard> selected)
        {
            var selectedArray = selected.ToArray();
            SelectionState.StateControl.StopSelection();

            _currentTable.Deck.AddCards(selectedArray);
            _currentTable.Deck.Shuffle();

            foreach (var card in _cards.Where(t => !selectedArray.Contains(t)).ToArray())
                _currentTable.Hand.TryAddCard(card);

            yield return WaitBetweenActions;

            for (int i = 0; i < selectedArray.Count(); i++)
            {
                TryPutCardToHandFromDeck(_currentTable);
            }

            yield return WaitBetweenActions;

            GoToNextState();
        }

        private void GoToNextState()
        {
            if (CurrentPlayer == Player.One)
                TurnStateControl.GoToSelectStartHand(Player.Two);
            else
                TurnStateControl.GoToNormalTurns();
        }


        private static bool TryPutCardToHandFromDeck(ITableSide table)
        {
            if (!table.Deck.TryGetCard(out var card))
                return false;

            if (!table.Hand.TryAddCard(card))
            {
                table.Deck.AddCards(card);
                return false;
            }

            return true;
        }
    }
}