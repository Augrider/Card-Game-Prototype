using System;
using Game.Entities.Cards;
using Game.Entities.Tables;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Plugins.Input
{
    internal class NullInputProvider : IInputProvider
    {
        public bool InputEnabled { get => false; set => throw new NotImplementedException("Null Input"); }

        public Vector3 PointerPosition => Vector3.zero;

        public event Action<ICard> CardSelected;
        public event Action<IUnit> UnitSelected;
        public event Action<IUnitPlace> UnitPlaceSelected;

        public event Action Cancelled;

        public void InvokeCancelEvent()
        {
            throw new NotImplementedException();
        }

        public void InvokeCardSelectedEvent(ICard card)
        {
            throw new NotImplementedException();
        }

        public void InvokeUnitPlaceSelectedEvent(IUnitPlace unitPlace)
        {
            throw new NotImplementedException();
        }

        public void InvokeUnitSelectedEvent(IUnit unit)
        {
            throw new NotImplementedException();
        }
    }
}