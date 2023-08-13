using Game.Actions;
using Game.Entities.Cards;
using Game.Entities.Tables;
using Game.Entities.Units;
using Game.Selection;
using UnityEngine;

namespace Game.Turn
{
    public class DefaultTurnState : TurnState
    {
        private ITableSide _currentTable;

        public static ITargetData DefaultTargetData { get; set; }


        public override void OnStateEnter()
        {
            //Increase max mana, refresh it and get card from deck if not full hand
            //Also all units on table get refreshed move

            _currentTable = Table.GetTableSide(GameState.GameState.CurrentPlayer);
            _currentTable.Hand.ToggleHide(false);

            RefreshMana(_currentTable);
            DrawCard(_currentTable);
            RefreshUnits(_currentTable);

            //Subscribe to select object events
            //Start selection
            SelectionState.StateControl.SetCurrentTable(_currentTable);

            //When card selected - Add Play Card on stack
            //When unit selected - Add Attack Unit on stack
            StartSelection();

            Actions.Action.ActionStack.StackGotEmpty += OnStackEmpty;
        }

        public override void OnStateExit()
        {
            SelectionState.StateControl.StopSelection();
            UnsubscribeFromSelection();

            Actions.Action.ActionStack.StackGotEmpty -= OnStackEmpty;

            _currentTable.Hand.ToggleHide(true);
        }



        private void RefreshUnits(ITableSide tableSide)
        {
            foreach (var unit in tableSide.Units)
                unit.State.ReadyToAttack = true;
            tableSide.HeroUnit.State.ReadyToAttack = true;
        }

        private void DrawCard(ITableSide tableSide)
        {
            if (!tableSide.Hand.IsFull && tableSide.Deck.TryGetCard(out var card))
                tableSide.Hand.TryAddCard(card);
        }

        private void RefreshMana(ITableSide tableSide)
        {
            tableSide.MaxMana++;
            tableSide.Mana = tableSide.MaxMana;
        }


        private void StartSelection()
        {
            SelectionState.StateControl.GoToSelectObject();

            Selection.SelectObjectState.CardSelected += OnCardSelected;
            Selection.SelectObjectState.UnitSelected += OnUnitSelected;
        }

        private void UnsubscribeFromSelection()
        {
            Selection.SelectObjectState.CardSelected -= OnCardSelected;
            Selection.SelectObjectState.UnitSelected -= OnUnitSelected;
        }


        private void OnUnitSelected(IUnit unit)
        {
            if (unit.Owner != _currentTable.Player)
                return;

            if (!unit.State.ReadyToAttack || unit.State.UnitAttack <= 0)
            {
                Debug.Log("Unit cannot attack now!");
                return;
            }

            Debug.Log($"Selected {unit}");
            UnsubscribeFromSelection();

            AttackUnitAction.TargetData = DefaultTargetData;
            Actions.Action.ActionStack.PutOnStack(new AttackUnitAction(unit));
        }

        private void OnCardSelected(ICard card)
        {
            if (card.Owner != _currentTable.Player)
                return;

            if (_currentTable.Mana < card.ManaCost)
            {
                Debug.Log("Not enough mana!");
                return;
            }

            Debug.Log($"Selected {card}");
            UnsubscribeFromSelection();

            Actions.Action.ActionStack.PutOnStack(new PlayCardAction(_currentTable.Player, card));
        }


        private void OnStackEmpty()
        {
            //Start selection again
            Debug.Log("Stack empty");
            StartSelection();
        }
    }
}