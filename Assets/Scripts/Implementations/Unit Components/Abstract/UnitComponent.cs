using System.Collections;
using System.Collections.Generic;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Components.Unit
{
    public class UnitComponent : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        protected IUnit Unit => _unit;
    }
}