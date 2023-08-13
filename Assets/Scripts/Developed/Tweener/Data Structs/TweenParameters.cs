using System;
using UnityEngine;


namespace Developed.TweenSystem
{
    [Serializable]
    public struct TweenParameters
    {
        [SerializeField] private float _length;
        [SerializeField] private AnimationCurve _curve;

        public float Length => _length;


        public TweenParameters(float length)
        {
            this._length = length;
            this._curve = AnimationCurve.Linear(0, 0, 1, 1);
        }

        public TweenParameters(float length, AnimationCurve curve)
        {
            this._length = length;
            this._curve = curve;
        }


        public float GetLerpValue(float currentTime)
        {
            var lerpUnclamped = currentTime / Length;
            return _curve.Evaluate(lerpUnclamped);
        }

        public bool IsFixedTimeFinished(float currentTime)
        {
            var lerpUnclamped = currentTime / Length;
            var isFinite = (_curve.postWrapMode & (WrapMode.ClampForever | WrapMode.Clamp | WrapMode.Once | WrapMode.Default)) > 0;

            return lerpUnclamped >= 1 && isFinite;
        }
    }
}