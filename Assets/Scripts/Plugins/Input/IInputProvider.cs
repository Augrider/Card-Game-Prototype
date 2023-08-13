using System;
using Game.Entities.Cards;
using Game.Entities.Tables;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Plugins.Input
{
    public interface IInputProvider
    {
        event Action<ICard> CardSelected;
        event Action<IUnit> UnitSelected;
        event Action<IUnitPlace> UnitPlaceSelected;

        event Action Cancelled;

        bool InputEnabled { get; set; }

        Vector3 PointerPosition { get; }

        void InvokeCardSelectedEvent(ICard card);
        void InvokeUnitSelectedEvent(IUnit unit);
        void InvokeUnitPlaceSelectedEvent(IUnitPlace unitPlace);

        void InvokeCancelEvent();
    }
}