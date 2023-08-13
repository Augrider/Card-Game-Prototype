using System.Collections;
using System.Collections.Generic;
using Developed.TweenSystem;
using UnityEngine;

namespace Game.Components
{
    [System.Serializable]
    public class MovementTween : ITween
    {
        [SerializeField] private Transform _positionTransform;
        [SerializeField] private Transform _rotationTransform;
        [SerializeField] private Transform _scaleTransform;

        [SerializeField] private MovementState _startPoint;
        [SerializeField] private MovementState _endPoint;

        [SerializeField] private TweenParameters _parameters;

        public TweenParameters Parameters { get => _parameters; set => _parameters = value; }


        public void SetStartPoint(MovementState start) => _startPoint = start;
        public void SetEndPoint(MovementState end) => _endPoint = end;


        public void ChangeState(float lerpValue)
        {
            _positionTransform.position = Vector3.LerpUnclamped(_startPoint.Position, _endPoint.Position, lerpValue);
            _rotationTransform.rotation = Quaternion.LerpUnclamped(_startPoint.Rotation, _endPoint.Rotation, lerpValue);
            _scaleTransform.localScale = Vector3.LerpUnclamped(_startPoint.Scale, _endPoint.Scale, lerpValue);
        }
    }



    [System.Serializable]
    public struct MovementState
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;


        public MovementState(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
    }
}