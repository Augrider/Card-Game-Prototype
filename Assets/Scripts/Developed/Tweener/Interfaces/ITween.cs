using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Developed.TweenSystem
{
    public interface ITween
    {
        TweenParameters Parameters { get; }

        void ChangeState(float lerpValue);
    }
}