using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities.Units
{
    public interface ITargetData
    {
        TargetAlignment Alignment { get; }
        //Method of target choosing: all, random, manual

        //Means to get all allowed target units for provided Table side
        //Ideally, only one class needed
        IEnumerable<IUnit> GetAllowedTargets(Tables.ITableSide tableSide);
    }
}