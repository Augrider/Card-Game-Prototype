using System.Collections.Generic;
using Game.Entities.Cards;

namespace Game.Entities.Tables
{
    public interface ICardHand
    {
        int CardAmount { get; }
        bool IsFull { get; }

        IEnumerable<ICard> Cards { get; }

        /// <summary>
        /// Try to add card to hand and move it on the spot if able
        /// </summary>
        /// <param name="card">Card that we try to put into this hand</param>
        bool TryAddCard(ICard card);

        /// <summary>
        /// Try to get card from hand
        /// </summary>
        /// <param name="card">Card that we try to get from this hand</param>
        bool TryTakeCard(ICard card);

        /// <summary>
        /// Remove all cards from hand records
        /// </summary>
        void Clear();

        /// <summary>
        /// Show/Hide all cards
        /// </summary>
        void ToggleHide(bool value);
    }
}