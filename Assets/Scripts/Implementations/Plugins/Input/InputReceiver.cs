using System;
using Game.Entities.Cards;
using Game.Entities.Tables;
using Game.Entities.Units;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Plugins.Input
{
    public sealed class InputReceiver : MonoBehaviour, IInputProvider
    {
        [SerializeField] private Camera _camera;

        public event Action<ICard> CardSelected;
        public event Action<IUnit> UnitSelected;
        public event Action<IUnitPlace> UnitPlaceSelected;

        public event Action Cancelled;

        public Vector3 PointerPosition => GetPointerPosition(Mouse.current.position.ReadValue());

        public bool InputEnabled { get; set; }


        void Awake()
        {
            InputLocator.Provide(this);
        }

        void OnDestroy()
        {
            InputLocator.Provide(null);
        }


        public void InvokeCardSelectedEvent(ICard card)
        {
            CardSelected?.Invoke(card);
        }

        public void InvokeUnitSelectedEvent(IUnit unit)
        {
            UnitSelected?.Invoke(unit);
        }

        public void InvokeUnitPlaceSelectedEvent(IUnitPlace unitPlace)
        {
            UnitPlaceSelected?.Invoke(unitPlace);
        }


        public void InvokeCancelEvent()
        {
            Cancelled?.Invoke();
        }



        private Vector3 GetPointerPosition(Vector2 devicePosition)
        {
            var devicePosition3D = new Vector3(devicePosition.x, devicePosition.y, -_camera.transform.position.z);
            return _camera.ScreenToWorldPoint(devicePosition3D);
        }
    }
}