using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Game.Entities.Tables;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Components.Table
{
    public sealed class TableSide : MonoBehaviour, ITableSide
    {
        [SerializeField] private Player _playerSide;

        [SerializeField] private Unit.Unit _playerUnit;
        [SerializeField] private UnitPlace _heroPlace;
        [SerializeField] private UnitPlace[] _unitPlaces;

        [SerializeField] private CardDeck _cardDeck;
        [SerializeField] private CardDeck _cardGraveyard;
        [SerializeField] private CardHand _cardHand;

        [SerializeField] private ManaCounter _manaCounter;

        [SerializeField] private Bounds _handBounds;
        [SerializeField] private Bounds _unitBounds;

        public Player Player => _playerSide;

        public int Mana { get => _manaCounter.Mana; set => _manaCounter.SetCounter(value, MaxMana); }
        public int MaxMana { get => _manaCounter.MaxMana; set => _manaCounter.SetCounter(Mana, value); }

        public IEnumerable<IUnitPlace> UnitPlaces => _unitPlaces;
        public IEnumerable<IUnit> Units => UnitPlaces.Where(t => t.Occupied).Select(t => t.Unit);
        public IUnitPlace HeroUnitPlace => _heroPlace;
        public IUnit HeroUnit => _playerUnit;

        public ICardDeck Deck => _cardDeck;
        public ICardDeck Graveyard => _cardGraveyard;
        public ICardHand Hand => _cardHand;


        void Awake()
        {
            Entities.Tables.Table.Provide(this, Player);
        }

        void OnDestroy()
        {
            Entities.Tables.Table.Provide(null, Player);
        }


        public Unit.Unit GetPlayerUnit() => _playerUnit;


        public TableArea GetClosestArea(Vector3 position)
        {
            var position2d = position;
            position2d.y = 0;

            if (_handBounds.Contains(position2d))
                return TableArea.Hand;

            if (_unitBounds.Contains(position2d))
                return TableArea.Units;

            return TableArea.None;
        }

        public bool TryGetClosestFreeUnitPlace(Vector3 position, out IUnitPlace place)
        {
            place = null;

            if (UnitPlaces.All(t => t.Occupied))
                return false;

            place = UnitPlaces.OrderBy(t => (t.Position - position).sqrMagnitude).First(t => !t.Occupied);
            return true;
        }


        public void PlaceUnit(IUnit unit, IUnitPlace place)
        {
            place.Unit = unit;

            unit?.UnitMovements.MoveTo(place.Position);
        }
    }
}