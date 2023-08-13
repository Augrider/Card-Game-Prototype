using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Components.Effects;
using Game.Entities.Cards;
using Game.Entities.Tables;
using UnityEngine;

namespace Game.Components.Card
{
    [CreateAssetMenu(menuName = "Cards/Unit")]
    public class UnitCardProvider : CardProvider
    {
        [SerializeField] private UnitStats _unitStats;

        [SerializeField] private BaseUnitEffectProvider[] _onPlacedEffects;
        [SerializeField] private TargetableUnitEffectProvider[] _onTableEffects;

        public override bool IsUnitCard => true;

        //TODO: Unit effects


        protected override void SetCardSpecificParameters(Card cardObject)
        {
            if (!(cardObject is UnitCard unitCard))
                throw new System.NullReferenceException("Unit card was not found!");

            var unitCardVisualComponent = cardObject.GetComponent<UnitCardVisualComponent>();

            unitCard.UnitStats = _unitStats;
            unitCard.CardEffect = new PlaceUnitEffect(unitCard);

            unitCard.UnitPlacedEffects = _onPlacedEffects.Select(t => t.GetEffect()).ToArray();
            unitCard.UnitOnTableEffects = _onTableEffects.Select(t => t.GetTargetableEffect()).ToArray();

            unitCardVisualComponent.SetUnitType(CommonFunctions.GetUnitTypeString(_unitStats.UnitType));
        }
    }
}