using System.Collections;
using System.Collections.Generic;
using Game.Entities.Cards;
using Game.Entities.Tables;
using Game.Entities.Units;
using Game.Plugins.Input;
using Game.Selection;
using UnityEngine;

namespace Game.Controller
{
    public class SelectionController : MonoBehaviour, ISelectionStateControl
    {
        [SerializeField] private BaseCardsSelectionScreen _cardsSelectionScreen;

        // private DragCardState _dragCardState = new DragCardState();

        private SelectObjectState _selectObjectState = new SelectObjectState();
        private SelectTargetUnitState _selectTargetState = new SelectTargetUnitState();
        private SelectUnitSpotState _selectUnitSpotState = new SelectUnitSpotState();
        private SelectCardsState _selectCardsState;

        private ISelectionState _currentState;
        private bool _isMovingCard;


        // Start is called before the first frame update
        void Awake()
        {
            SelectionState.StateControl = this;

            _selectCardsState = new SelectCardsState(_cardsSelectionScreen);
        }

        void OnDestroy()
        {
            SelectionState.StateControl = null;
        }


        public void SetCurrentTable(ITableSide selectionTable)
        {
            SelectionState.CurrentTable = selectionTable;
        }


        public void GoToSelectObject()
        {
            InputLocator.Service.InputEnabled = true;

            ChangeState(_selectObjectState);
        }

        public void GoToSelectTargetUnit(Vector3 lineStart, params IUnit[] selectableUnits)
        {
            InputLocator.Service.InputEnabled = true;
            SelectionState.SelectionVisuals.StartDrawingLineToPointer(lineStart);

            _selectTargetState.SelectableUnits = selectableUnits;

            ChangeState(_selectTargetState);
        }

        public void GoToSelectUnitSpot(Vector3 lineStart)
        {
            InputLocator.Service.InputEnabled = true;
            SelectionState.SelectionVisuals.StartDrawingLineToPointer(lineStart);

            ChangeState(_selectUnitSpotState);
        }

        public void GoToSelectCardsFrom(params ICard[] cards)
        {
            InputLocator.Service.InputEnabled = true;

            _selectCardsState.SelectableCards = cards;

            ChangeState(_selectCardsState);
        }


        public void StopSelection()
        {
            InputLocator.Service.InputEnabled = false;

            SelectionState.SelectionVisuals.StopDrawingLine();

            ChangeState(null);
        }



        private void ChangeState(ISelectionState newState)
        {
            _currentState?.OnStateExit();
            newState?.OnStateEnter();

            _currentState = newState;
        }
    }
}