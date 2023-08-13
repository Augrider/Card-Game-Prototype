using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Developed.TweenSystem.Tweens
{
    [CreateAssetMenu(menuName = "Tweener/Tweens/Position", order = 51)]
    public class EffectMove : TweenProvider
    {
        [SerializeField] private Vector3 _startPoint;
        [SerializeField] private Vector3 _endPoint;

        [SerializeField] private ControlAxis _controlledAxis;


        public override ITween GetTween(GameObject gameObject)
        {
            return GetCorrectTween(gameObject, Parameters);
        }

        public override ITween GetTween(GameObject gameObject, TweenParameters tweenParameters)
        {
            return GetCorrectTween(gameObject, tweenParameters);
        }



        private ITween GetCorrectTween(GameObject gameObject, TweenParameters parameters)
        {
            if (gameObject.TryGetComponent<RectTransform>(out var rectTransform))
                return GetRectMoveTween(rectTransform, parameters);
            else
                return GetMoveTween(gameObject.transform, parameters);
        }


        private ITween GetRectMoveTween(RectTransform rectTransform, TweenParameters parameters)
        {
            var result = new MoveTween_Rect(rectTransform, parameters);

            result.StartPoint = _startPoint;
            result.EndPoint = _endPoint;
            result.ControlledAxis = _controlledAxis;

            return result;
        }

        private ITween GetMoveTween(Transform transform, TweenParameters parameters)
        {
            var result = new MoveTween(transform, parameters);

            result.StartPoint = _startPoint;
            result.EndPoint = _endPoint;
            result.ControlledAxis = _controlledAxis;

            return result;
        }
    }
}