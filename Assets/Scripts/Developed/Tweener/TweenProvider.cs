using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Developed.TweenSystem
{
    public abstract class TweenProvider : ScriptableObject
    {
        [SerializeField] private TweenParameters _tweenParameters;
        public TweenParameters Parameters => _tweenParameters;

        public abstract ITween GetTween(GameObject gameObject);
        public abstract ITween GetTween(GameObject gameObject, TweenParameters tweenParameters);
    }
}