using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    /// <summary>
    /// Handles object movements to position, rotation and scale
    /// </summary>
    public interface IMovement
    {
        bool IsMoving { get; }

        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        Vector3 Scale { get; set; }

        void MoveTo(Vector3 target);
        void RotateTo(Quaternion target);
        void ChangeScaleTo(Vector3 target);
    }
}