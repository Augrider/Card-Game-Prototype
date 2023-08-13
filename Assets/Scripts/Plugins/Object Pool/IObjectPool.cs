using System.Collections;
using System.Collections.Generic;
using Game.Entities.Cards;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Plugins.ObjectPool
{
    public interface IObjectPool
    {
        ICard GetNewCard(ICardProvider cardProvider, Player owner, Vector3 position, Quaternion rotation);
        // void RemoveCard(ICard card);

        IUnit GetNewUnit(Player owner, IUnitSpawnData spawnData, Vector3 position, Quaternion rotation);
        IEnumerable<IUnit> GetActiveUnits();
        void RemoveUnit(IUnit unit);

        void RemoveDeadUnits();
    }
}