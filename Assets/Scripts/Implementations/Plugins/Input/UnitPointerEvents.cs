using Game.Components.Unit;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Plugins.Input
{
    public class UnitPointerEvents : UnitComponent, IPointerUpHandler
    {
        public void OnPointerUp(PointerEventData eventData)
        {
            if (InputLocator.Service.InputEnabled)
                InputLocator.Service.InvokeUnitSelectedEvent(Unit);
        }
    }
}