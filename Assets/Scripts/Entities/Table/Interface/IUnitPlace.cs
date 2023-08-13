using Game.Entities.Units;
using UnityEngine;

namespace Game.Entities.Tables
{
    public interface IUnitPlace
    {
        Vector3 Position { get; }
        Quaternion Rotation { get; }

        bool Occupied { get; }
        IUnit Unit { get; set; }
    }
}