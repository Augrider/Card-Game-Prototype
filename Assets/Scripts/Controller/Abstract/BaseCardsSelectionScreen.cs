using System;
using System.Collections.Generic;
using Game.Entities.Cards;
using UnityEngine;

namespace Game.Selection
{
    public abstract class BaseCardsSelectionScreen : MonoBehaviour, ISelectCardsScreen
    {
        public abstract bool Enabled { get; set; }
        public abstract int MaxSelected { get; set; }

        public IEnumerable<ICard> SelectedCards { get; protected set; } = new ICard[0];
        public IEnumerable<ICard> AllCards { get; protected set; } = new ICard[0];

        public event Action SelectionConfirmed;

        public abstract void SetActivePlayer(Player player);
        public abstract void SetCards(params ICard[] cards);


        public void ConfirmSelection() => SelectionConfirmed?.Invoke();
    }
}