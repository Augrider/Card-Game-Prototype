using System.Collections;
using System.Collections.Generic;
using Game.Entities.Cards;
using Game.Entities.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.Card
{
    public class UnitCardVisualComponent : CardVisualComponent
    {
        [SerializeField] private TextMeshProUGUI _unitType;
        [SerializeField] private TextMeshProUGUI _unitAttack;
        [SerializeField] private TextMeshProUGUI _unitHealth;

        [SerializeField] private Image _taunt;
        [SerializeField] private Image _charge;

        private IUnitCard UnitCard => Card as IUnitCard;


        //TODO: Card state changed event and automatic stats update
        public void SetUnitStats(int attack, int health)
        {
            _unitAttack.SetText(attack.ToString());
            _unitHealth.SetText(health.ToString());
        }

        public void SetUnitType(string value)
        {
            _unitType.SetText(value);
        }


        protected override void OnStateChanged()
        {
            base.OnStateChanged();

            SetUnitStats(UnitCard.UnitStats.UnitAttack, UnitCard.UnitStats.UnitMaxHealth);
        }
    }
}