using System.Collections;
using System.Collections.Generic;
using Game.Actions;
using Game.Entities;
using Game.Entities.Tables;
using UnityEngine;

namespace Game.Components.Effects
{
    [CreateAssetMenu(menuName = "Effects/Draw Card", order = 0)]
    public class DrawCardEffectProvider : BaseUnitEffectProvider
    {
        [SerializeField] private int _amount;


        public override IEffectProvider GetEffect()
        {
            return new DrawCardEffect(_amount);
        }
    }



    public class DrawCardEffect : IEffectProvider
    {
        private int _amount;


        public DrawCardEffect(int amount)
        {
            _amount = amount;
        }


        public IAction GetAction(Player player)
        {
            return new DoAction(Table.GetTableSide(player), DrawCard);
        }



        private void DrawCard(ITableSide tableSide)
        {
            if (tableSide.Deck.TryGetCard(out var card) && !tableSide.Hand.TryAddCard(card))
                tableSide.Graveyard.AddCards(card);
        }
    }
}