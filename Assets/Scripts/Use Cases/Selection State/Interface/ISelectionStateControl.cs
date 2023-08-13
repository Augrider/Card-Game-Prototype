using Game.Entities.Cards;
using Game.Entities.Tables;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Selection
{
    public interface ISelectionStateControl
    {
        void SetCurrentTable(ITableSide selectionTable);

        void GoToSelectObject();
        void GoToSelectUnitSpot(Vector3 lineStart);
        void GoToSelectTargetUnit(Vector3 lineStart, params IUnit[] selectableUnits);
        void GoToSelectCardsFrom(params ICard[] cards);

        void StopSelection();
    }
}