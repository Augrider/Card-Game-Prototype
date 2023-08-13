using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entities.Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.Unit
{
    public class UnitVisuals : UnitComponent
    {
        // [SerializeField] private TextMeshProUGUI _name;

        [SerializeField] private Image _portrait;

        [SerializeField] private TextMeshProUGUI _unitAttack;
        [SerializeField] private TextMeshProUGUI _unitHealth;

        [SerializeField] private CanvasGroup _taunt;
        [SerializeField] private CanvasGroup _charge;

        //Charge and taunt
        //Highlight?


        void OnEnable()
        {
            Unit.StateChanged += OnStateChanged;
            OnStateChanged();
        }

        void OnDisable()
        {
            Unit.StateChanged -= OnStateChanged;
        }


        public void SetUnitStats(int attack, int health)
        {
            _unitAttack.SetText(attack.ToString());
            _unitHealth.SetText(health.ToString());
        }


        public void SetUnitVisuals(CardVisuals cardVisuals)
        {
            _portrait.sprite = cardVisuals.CardPortrait;
        }



        private void OnStateChanged()
        {
            if (Unit.State == null)
                return;

            SetUnitStats(Unit.State.UnitAttack, Unit.State.UnitHealth);

            _charge.alpha = Unit.State.Charge ? 1 : 0;
            _taunt.alpha = Unit.State.Taunt ? 1 : 0;
        }
    }
}