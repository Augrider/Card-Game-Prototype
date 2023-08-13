using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.Plugins.Input
{
    public sealed class PointerEventsReceiver : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private UnityEvent _PointerClick;
        [SerializeField] private UnityEvent _PointerEnter;
        [SerializeField] private UnityEvent _PointerExit;


        public void OnPointerClick(PointerEventData eventData) => _PointerClick?.Invoke();
        public void OnPointerEnter(PointerEventData eventData) => _PointerEnter?.Invoke();
        public void OnPointerExit(PointerEventData eventData) => _PointerExit?.Invoke();

    }
}