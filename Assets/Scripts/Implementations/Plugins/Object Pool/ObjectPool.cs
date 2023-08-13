using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Components.Card;
using Game.Components.Unit;
using Game.Entities.Cards;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Plugins.ObjectPool
{
    public class ObjectPool : MonoBehaviour, IObjectPool
    {
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private UnitCard _unitCardPrefab;

        [SerializeField] private Unit _unitPrefab;

        [SerializeField] private Transform _cardParent;
        [SerializeField] private Transform _unitParent;

        private Dictionary<UnitRecord, bool> _units = new Dictionary<UnitRecord, bool>();
        // private List<CardRecord> _cards;


        void Awake()
        {
            ObjectPoolLocator.Provide(this);
        }

        void OnDestroy()
        {
            ObjectPoolLocator.Provide(null);
        }


        public ICard GetNewCard(ICardProvider cardProvider, Player owner, Vector3 position, Quaternion rotation)
        {
            GameObject cardObject;
            Card card;

            if (cardProvider.IsUnitCard)
                card = Instantiate<UnitCard>(_unitCardPrefab, _cardParent);
            else
                card = Instantiate<Card>(_cardPrefab, _cardParent);

            cardObject = card.gameObject;
            cardProvider.SetCardParameters(cardObject, owner);

            card.Owner = owner;

            card.CardMovements.Position = position;
            card.CardMovements.Rotation = rotation;

            return card;
        }


        public IUnit GetNewUnit(Player owner, IUnitSpawnData spawnData, Vector3 position, Quaternion rotation)
        {
            if (!TryGetAvailableUnit(out var record))
                record = GetNewRecord();

            _units[record] = true;
            record.RecordUnit.gameObject.SetActive(true);

            record.RecordUnit.UnitMovements.Position = position;
            record.RecordUnit.UnitMovements.Rotation = rotation;

            SetUnitStats(record, owner, spawnData);
            return record.RecordUnit;
        }

        public IEnumerable<IUnit> GetActiveUnits()
        {
            return _units.Keys.Where(t => _units[t]).Select(t => t.RecordUnit);
        }

        public void RemoveUnit(IUnit unit)
        {
            var record = _units.First(t => t.Key.RecordUnit == unit).Key;

            _units[record] = false;
            record.RecordUnit.gameObject.SetActive(false);
        }


        public void RemoveDeadUnits()
        {
            Debug.LogWarning("Cleaning dead!");

            foreach (var key in _units.Keys.ToArray())
                if (_units[key] && key.RecordUnit.State.UnitHealth <= 0)
                    RemoveUnit(key.RecordUnit);
        }



        private UnitRecord GetNewRecord()
        {
            var unitObject = Instantiate<Unit>(_unitPrefab, _unitParent);
            var unitVisuals = unitObject.GetComponent<UnitVisuals>();

            var unitRecord = new UnitRecord(unitObject, unitVisuals);
            _units.Add(unitRecord, false);

            return unitRecord;
        }

        private bool TryGetAvailableUnit(out UnitRecord record)
        {
            record = default;

            foreach (var key in _units.Keys)
                if (!_units[key])
                {
                    record = key;
                    return true;
                }

            return false;
        }


        private void SetUnitStats(UnitRecord record, Player owner, IUnitSpawnData spawnData)
        {
            record.Visuals.SetUnitVisuals(spawnData.VisualStats);

            record.RecordUnit.Owner = owner;
            record.RecordUnit.State.ReplaceFrom(spawnData.UnitStats);

            record.RecordUnit.UnitPlacedEffects = spawnData.UnitPlacedEffects;
            record.RecordUnit.UnitOnTableEffects = spawnData.UnitOnTableEffects;
        }



        private struct UnitRecord
        {
            public Unit RecordUnit;
            public UnitVisuals Visuals;

            public UnitRecord(Unit unit, UnitVisuals visuals)
            {
                RecordUnit = unit;
                Visuals = visuals;
            }
        }
    }
}