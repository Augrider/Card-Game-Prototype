using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Developed.TweenSystem.Tweens
{
    [System.Serializable]
    public class MoveTween : ITween
    {
        [SerializeField] private Transform _transform;

        [SerializeField] private Vector3 _startPoint;
        [SerializeField] private Vector3 _endPoint;

        [SerializeField] private ControlAxis _controlledAxis;
        [SerializeField] private bool _useLocal;

        [SerializeField] private TweenParameters _parameters;

        public Vector3 StartPoint { get => _startPoint; set => _startPoint = value; }
        public Vector3 EndPoint { get => _endPoint; set => _endPoint = value; }

        public ControlAxis ControlledAxis { get => _controlledAxis; set => _controlledAxis = value; }
        public bool UseLocal { get => _useLocal; set => _useLocal = value; }

        public TweenParameters Parameters { get => _parameters; set => _parameters = value; }


        public MoveTween(Transform transform, TweenParameters tweenParameters)
        {
            this._transform = transform;
            this.Parameters = tweenParameters;
        }


        public void ChangeState(float lerpValue)
        {
            if (UseLocal)
                _transform.localPosition = GetCorrectPosition(_transform.localPosition, lerpValue);
            else
                _transform.position = GetCorrectPosition(_transform.position, lerpValue);
        }



        private Vector3 GetCorrectPosition(Vector3 currentPosition, float lerpValue)
        {
            var fullControlledVector = Vector3.LerpUnclamped(StartPoint, EndPoint, lerpValue);

            return new Vector3(ControlledAxis.ControlX ? fullControlledVector.x : currentPosition.x,
                ControlledAxis.ControlY ? fullControlledVector.y : currentPosition.y,
                ControlledAxis.ControlZ ? fullControlledVector.z : currentPosition.z);
        }
    }



    [SerializeField]
    public class MoveTween_Rect : ITween
    {
        [SerializeField] private RectTransform _transform;

        [SerializeField] private Vector3 _startPoint;
        [SerializeField] private Vector3 _endPoint;

        [SerializeField] private ControlAxis _controlledAxis;

        [SerializeField] private TweenParameters _parameters;

        public Vector3 StartPoint { get => _startPoint; set => _startPoint = value; }
        public Vector3 EndPoint { get => _endPoint; set => _endPoint = value; }

        public ControlAxis ControlledAxis { get => _controlledAxis; set => _controlledAxis = value; }

        public TweenParameters Parameters { get => _parameters; set => _parameters = value; }


        public MoveTween_Rect(RectTransform transform, TweenParameters tweenParameters)
        {
            this._transform = transform;
            this.Parameters = tweenParameters;
        }


        public void ChangeState(float lerpValue)
        {
            _transform.anchoredPosition = GetCorrectPosition(_transform.position, lerpValue);
        }



        private Vector3 GetCorrectPosition(Vector3 currentPosition, float lerpValue)
        {
            var fullControlledVector = Vector3.LerpUnclamped(StartPoint, EndPoint, lerpValue);

            return new Vector3(ControlledAxis.ControlX ? fullControlledVector.x : currentPosition.x,
                ControlledAxis.ControlY ? fullControlledVector.y : currentPosition.y,
                ControlledAxis.ControlZ ? fullControlledVector.z : currentPosition.z);
        }
    }
}