using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Components.Table
{
    public sealed class ManaCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counterText;

        [SerializeField] private UnityEvent _stateChanged;

        public int Mana { get; private set; }
        public int MaxMana { get; private set; }


        public void SetCounter(int mana, int maxMana)
        {
            Mana = mana;
            MaxMana = maxMana;

            counterText.SetText($"{mana}/{maxMana}");
            _stateChanged?.Invoke();
        }
    }
}