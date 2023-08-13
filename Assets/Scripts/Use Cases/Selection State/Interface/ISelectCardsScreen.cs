using System.Collections.Generic;
using Game.Entities.Cards;

namespace Game.Selection
{
    public interface ISelectCardsScreen
    {
        bool Enabled { get; set; }

        IEnumerable<ICard> SelectedCards { get; }
        IEnumerable<ICard> AllCards { get; }
        int MaxSelected { get; set; }

        event System.Action SelectionConfirmed;

        void SetActivePlayer(Player player);
        void SetCards(params ICard[] cards);
    }
}