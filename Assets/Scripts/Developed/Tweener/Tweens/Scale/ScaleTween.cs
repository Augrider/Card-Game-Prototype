using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Developed.TweenSystem.Tweens
{
    [System.Serializable]
    public class ScaleTween : ITween
    {
        [SerializeField] private Transform _transform;

        [SerializeField] private Vector3 _startScale;
        [SerializeField] private Vector3 _endScale;

        [SerializeField] private ControlAxis _controlledAxis;

        [SerializeField] private TweenParameters _parameters;

        public Vector3 StartScale { get => _startScale; set => _startScale = value; }
        public Vector3 EndScale { get => _endScale; set => _endScale = value; }

        public ControlAxis ControlledAxis { get => _controlledAxis; set => _controlledAxis = value; }

        public TweenParameters Parameters { get => _parameters; set => _parameters = value; }


        public ScaleTween(Transform transform, TweenParameters tweenParameters)
        {
            this._transform = transform;
            this.Parameters = tweenParameters;
        }


        public void ChangeState(float lerpValue)
        {
            _transform.localScale = GetCorrectScale(_transform.localScale, lerpValue);
        }



        private Vector3 GetCorrectScale(Vector3 current, float lerpValue)
        {
            var fullControlledVector = Vector3.LerpUnclamped(StartScale, EndScale, lerpValue);

            return new Vector3(ControlledAxis.ControlX ? fullControlledVector.x : current.x,
                ControlledAxis.ControlY ? fullControlledVector.y : current.y,
                ControlledAxis.ControlZ ? fullControlledVector.z : current.z);
        }
    }
}