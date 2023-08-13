using System.Collections;
using System.Collections.Generic;
using Game.Entities.Cards;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Plugins.ObjectPool
{
    internal class NullObjectPool : IObjectPool
    {
        public IEnumerable<IUnit> GetActiveUnits()
        {
            return new IUnit[0];
        }

        public ICard GetNewCard(ICardProvider cardProvider, Player owner, Vector3 position, Quaternion rotation)
        {
            throw new System.NotImplementedException();
        }

        public IUnit GetNewUnit(Player owner, IUnitSpawnData spawnData, Vector3 position, Quaternion rotation)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveDeadUnits()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveUnit(IUnit unit)
        {
            throw new System.NotImplementedException();
        }
    }
}