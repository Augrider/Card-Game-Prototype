using System.Collections.Generic;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Entities.Tables
{
    public interface ITableSide
    {
        Player Player { get; }

        int Mana { get; set; }
        int MaxMana { get; set; }

        IEnumerable<IUnitPlace> UnitPlaces { get; }
        IEnumerable<IUnit> Units { get; }

        IUnitPlace HeroUnitPlace { get; }
        IUnit HeroUnit { get; }

        ICardHand Hand { get; }
        ICardDeck Deck { get; }
        ICardDeck Graveyard { get; }

        TableArea GetClosestArea(Vector3 position);
        bool TryGetClosestFreeUnitPlace(Vector3 position, out IUnitPlace place);

        void PlaceUnit(IUnit unit, IUnitPlace place);
    }
}