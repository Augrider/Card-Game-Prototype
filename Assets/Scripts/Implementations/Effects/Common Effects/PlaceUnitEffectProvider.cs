using System.Linq;
using Game.Actions;
using Game.Entities;
using Game.Entities.Cards;
using Game.Entities.Tables;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Components.Effects
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "Effects/Place Unit", order = 0)]
    public class PlaceUnitEffectProvider : BaseUnitEffectProvider
    {
        //TODO: Using unit card provider would be easier
        [SerializeField] private CardVisuals _unitVisualStats;
        [SerializeField] private UnitStats _unitStats;

        [SerializeField] private BaseUnitEffectProvider[] _onPlacedEffects;
        [SerializeField] private TargetableUnitEffectProvider[] _onTableEffects;


        public override IEffectProvider GetEffect()
        {
            var spawnData = new UnitSpawnData(_unitVisualStats, _unitStats);

            spawnData.UnitPlacedEffects = _onPlacedEffects.Select(t => t.GetEffect()).ToArray();
            spawnData.UnitOnTableEffects = _onTableEffects.Select(t => t.GetTargetableEffect()).ToArray();

            return new PlaceUnitEffect(spawnData);
        }



        private class UnitSpawnData : IUnitSpawnData
        {

            public CardVisuals VisualStats { get; private set; }
            public UnitStats UnitStats { get; private set; }

            public IEffectProvider[] UnitPlacedEffects { get; set; }
            public ITargetableEffectProvider[] UnitOnTableEffects { get; set; }


            public UnitSpawnData(CardVisuals visualStats, UnitStats unitStats)
            {
                VisualStats = visualStats;
                UnitStats = unitStats;
            }
        }
    }



    public class PlaceUnitEffect : IEffectProvider
    {
        private IUnitSpawnData _spawnData;


        public PlaceUnitEffect(IUnitSpawnData spawnData)
        {
            _spawnData = spawnData;
        }


        public IAction GetAction(Player player)
        {
            return new PlaceUnitAction(_spawnData, Table.GetTableSide(player));
        }
    }
}