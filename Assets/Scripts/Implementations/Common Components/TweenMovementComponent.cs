using Developed.TweenSystem;
using Game.Entities;
using UnityEngine;

namespace Game.Components
{
    public class TweenMovementComponent : MonoBehaviour, IMovement
    {
        [SerializeField] private TweenComponent _tweenComponent;
        [SerializeField] private MovementTween _movementTween;

        private MovementState _endPoint;

        private bool _playNext = false;
        private bool _playMovement = false;
        private bool _playRotation = false;
        private bool _playScale = false;

        public bool IsMoving => _tweenComponent.IsPlaying();

        public Vector3 Position { get => transform.position; set => transform.position = value; }
        public Quaternion Rotation { get => transform.rotation; set => transform.rotation = value; }
        public Vector3 Scale { get => transform.localScale; set => transform.localScale = value; }


        private void LateUpdate()
        {
            if (!_playNext)
                return;

            if (_tweenComponent.IsPlaying(_movementTween))
            {
                _tweenComponent.Stop(_movementTween);
                _movementTween.ChangeState(1);
            }

            _movementTween.SetStartPoint(new MovementState(Position, Rotation, Scale));
            _movementTween.SetEndPoint(GetEndPoint(_endPoint));

            _tweenComponent.Play(_movementTween);

            ResetState();
        }


        public void MoveTo(Vector3 target)
        {
            _endPoint.Position = target;

            _playMovement = true;
            _playNext = true;
        }


        public void RotateTo(Quaternion target)
        {
            _endPoint.Rotation = target;

            _playRotation = true;
            _playNext = true;
        }


        public void ChangeScaleTo(Vector3 target)
        {
            _endPoint.Scale = target;

            _playScale = true;
            _playNext = true;
        }



        private void ResetState()
        {
            // _endPoint = new MovementState(Position, Rotation, Scale);
            // _startPoint =_endPoint;

            _playMovement = false;
            _playRotation = false;
            _playScale = false;

            _playNext = false;
        }

        private MovementState GetEndPoint(MovementState inputState)
        {
            var position = _playMovement ? inputState.Position : Position;
            var rotation = _playRotation ? inputState.Rotation : Rotation;
            var scale = _playScale ? inputState.Scale : Scale;

            return new MovementState(position, rotation, scale);
        }
    }
}