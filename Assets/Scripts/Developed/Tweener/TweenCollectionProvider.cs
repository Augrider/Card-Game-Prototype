using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Developed.TweenSystem
{
    [CreateAssetMenu(menuName = "Tween/Effect Collection", order = 51)]
    public class TweenCollectionProvider : TweenProvider
    {
        [SerializeField] private TweenProviderRecord[] _tweens;
        [SerializeField] private TweenParameters _defaultParameters;


        public override ITween GetTween(GameObject gameObject)
        {
            throw new NotImplementedException();
        }

        public override ITween GetTween(GameObject gameObject, TweenParameters tweenParameters)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ITween> GetTweenCollection(GameObject gameObject)
        {
            var result = new List<ITween>(_tweens.Length);

            foreach (var tweenRecord in _tweens)
            {
                if (tweenRecord.UseParametersFromTween)
                    result.Add(tweenRecord.Tween.GetTween(gameObject));
                else
                    result.Add(tweenRecord.Tween.GetTween(gameObject, Parameters));
            }

            return result;
        }



        private TweenRecord[] GetTweenRecords(GameObject gameObject)
        {
            var result = new List<TweenRecord>(_tweens.Length);

            foreach (var tweenRecord in _tweens)
            {
                result.Add(new TweenRecord(tweenRecord.Tween.GetTween(gameObject), tweenRecord.UseParametersFromTween));
            }

            return result.ToArray();
        }


        [System.Serializable]
        private struct TweenProviderRecord
        {
            public TweenProvider Tween;
            public bool UseParametersFromTween;
        }
    }
}