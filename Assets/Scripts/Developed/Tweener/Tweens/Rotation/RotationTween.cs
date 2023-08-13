using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Developed.TweenSystem.Tweens
{
    [System.Serializable]
    public class RotationTween : ITween
    {
        [SerializeField] private Transform _transform;

        [SerializeField] private Quaternion _startRotation;
        [SerializeField] private Quaternion _endRotation;

        [SerializeField] private bool _useLocal;
        [SerializeField] private TweenParameters _parameters;

        public Quaternion StartRotation { get => _startRotation; set => _startRotation = value; }
        public Quaternion EndRotation { get => _endRotation; set => _endRotation = value; }

        public TweenParameters Parameters { get => _parameters; set => _parameters = value; }


        public RotationTween(Transform transform, TweenParameters tweenParameters)
        {
            this._transform = transform;
            this.Parameters = tweenParameters;
        }


        public void ChangeState(float lerpValue)
        {
            if (_useLocal)
                _transform.localRotation = Quaternion.LerpUnclamped(StartRotation, EndRotation, lerpValue);
            else
                _transform.rotation = Quaternion.LerpUnclamped(StartRotation, EndRotation, lerpValue);
        }
    }



    [System.Serializable]
    public class RotationTween_Euler : ITween
    {
        [SerializeField] private Transform _transform;

        [SerializeField] private Vector3 _startAngles;
        [SerializeField] private Vector3 _endAngles;

        [SerializeField] private ControlAxis _controlledAxis;

        [SerializeField] private TweenParameters _parameters;

        public Vector3 StartAngles { get => _startAngles; set => _startAngles = value; }
        public Vector3 EndAngles { get => _endAngles; set => _endAngles = value; }

        public ControlAxis ControlledAxis { get => _controlledAxis; set => _controlledAxis = value; }

        public TweenParameters Parameters { get => _parameters; set => _parameters = value; }


        public RotationTween_Euler(Transform transform, TweenParameters tweenParameters)
        {
            this._transform = transform;
            this.Parameters = tweenParameters;
        }


        public void ChangeState(float lerpValue)
        {
            _transform.rotation = GetCorrectRotation(_transform.rotation, lerpValue);
        }



        //TODO: Better rotation calculations
        private Quaternion GetCorrectRotation(Quaternion current, float lerpValue)
        {
            var currentEuler = current.eulerAngles;

            var startVector = new Vector3(ControlledAxis.ControlX ? StartAngles.x : currentEuler.x,
                ControlledAxis.ControlY ? StartAngles.y : currentEuler.y,
                ControlledAxis.ControlZ ? StartAngles.z : currentEuler.z);

            var endVector = new Vector3(ControlledAxis.ControlX ? EndAngles.x : currentEuler.x,
                ControlledAxis.ControlY ? EndAngles.y : currentEuler.y,
                ControlledAxis.ControlZ ? EndAngles.z : currentEuler.z);

            var result = Quaternion.Euler(endVector - startVector);

            return result;
        }
    }
}