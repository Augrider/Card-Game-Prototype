using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    /// <summary>
    /// Handles object highlight
    /// </summary>
    public interface IHighlight
    {
        bool StrokeEnabled { get; set; }

        void ToggleLift(bool value);
        void ToggleHide(bool value);
        void ToggleTooltip(bool value);
    }
}