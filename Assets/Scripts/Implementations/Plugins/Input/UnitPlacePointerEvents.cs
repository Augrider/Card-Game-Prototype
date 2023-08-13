using Game.Components.Table;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Plugins.Input
{
    public class UnitPlacePointerEvents : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UnitPlace _unitPlace;


        public void OnPointerClick(PointerEventData eventData)
        {
            // Debug.Log("Click!");
            if (InputLocator.Service.InputEnabled)
                InputLocator.Service.InvokeUnitPlaceSelectedEvent(_unitPlace);
        }
    }
}