using System.Collections;
using System.Collections.Generic;
using Game.Entities.Cards;
using UnityEngine;

namespace Game.Entities.Tables
{
    public interface ICardDeck
    {
        int CardAmount { get; }

        void AddCards(params ICard[] cards);
        void Shuffle();

        /// <summary>
        /// Try to get card on top of deck
        /// </summary>
        bool TryGetCard(out ICard card);
    }
}