using System.Collections;
using System.Collections.Generic;
using Game.Entities.Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.Card
{
    public class CardVisualComponent : CardComponent
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _manaCost;
        [SerializeField] private TextMeshProUGUI _description;

        [SerializeField] private Image _portrait;


        void OnEnable()
        {
            Card.StateChanged += OnStateChanged;
            OnStateChanged();
        }

        void OnDisable()
        {
            Card.StateChanged -= OnStateChanged;
        }


        public void SetCardVisuals(CardVisuals cardVisuals)
        {
            _name.SetText(cardVisuals.CardName);
            _description.SetText(cardVisuals.Description);

            _portrait.sprite = cardVisuals.CardPortrait;
        }

        public void SetManaCost(int manaCost)
        {
            _manaCost.SetText(manaCost.ToString());
        }


        protected virtual void OnStateChanged()
        {
            SetManaCost(Card.ManaCost);
        }
    }
}