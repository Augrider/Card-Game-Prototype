using System.Collections;
using System.Collections.Generic;
using Game.Entities.Tables;
using Game.Entities.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.Table
{
    public sealed class UnitPlace : MonoBehaviour, IUnitPlace
    {
        [SerializeField] private Image _highlight;

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        public IUnit Unit { get; set; }
        public bool Occupied => Unit != null && Unit.State.UnitHealth > 0;


        public void ToggleHighlight(bool value)
        {
            var color = _highlight.color;
            color.a = value ? 1 : 0;

            _highlight.color = color;
        }
    }
}