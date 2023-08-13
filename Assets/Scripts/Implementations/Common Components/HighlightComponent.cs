using System;
using Developed.TweenSystem;
using Developed.TweenSystem.Tweens;
using Game.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components
{
    public class HighlightComponent : MonoBehaviour, IHighlight
    {
        [SerializeField] private Transform _pivotTransform;

        [SerializeField] private Image _stroke;

        [SerializeField] private TweenComponent _offsetTweenComponent;
        [SerializeField] private MoveTween _offsetTween;
        [SerializeField] private RotationTween _offsetRotationTween;
        [SerializeField] private ScaleTween _offsetScaleTween;

        [SerializeField] private Vector3 _hideAngle;
        [SerializeField] private Vector3 _liftOffset;
        [SerializeField] private Vector3 _tooltipOffset;
        [SerializeField] private Vector3 _tooltipScale;


        private bool _tooltip = false;
        private bool _lift = false;
        private bool _hide = false;

        public bool StrokeEnabled { get => _stroke.enabled; set => _stroke.enabled = value; }


        public void ToggleHide(bool value)
        {
            if (_hide == value)
                return;

            _hide = value;
            _tooltip = false;
            _lift = false;

            UpdateHighlightState();
        }


        public void ToggleLift(bool value)
        {
            if (_hide)
                return;

            if (_lift == value)
                return;

            _lift = value;

            UpdateHighlightState();
        }


        public void ToggleTooltip(bool value)
        {
            if (_hide)
                return;

            if (_tooltip == value)
                return;

            _tooltip = value;

            UpdateHighlightState();
        }


        public void UpdateHighlightState()
        {
            ChangeState(GetCorrectOffset(), GetCorrectRotation(), GetCorrectScale());
        }



        private Vector3 GetCorrectOffset()
        {
            if (_hide)
                return Vector3.zero;

            if (_tooltip)
                return _tooltipOffset;

            return _lift ? _liftOffset : Vector3.zero;
        }

        private Quaternion GetCorrectRotation()
        {
            return _hide ? Quaternion.Euler(_hideAngle) : Quaternion.identity;
        }

        private Vector3 GetCorrectScale()
        {
            return _tooltip ? _tooltipScale : Vector3.one;
        }


        private void ChangeState(Vector3 offset, Quaternion rotation, Vector3 scale)
        {
            _offsetTween.StartPoint = _pivotTransform.localPosition;
            _offsetRotationTween.StartRotation = _pivotTransform.localRotation;
            _offsetScaleTween.StartScale = _pivotTransform.localScale;

            _offsetTween.EndPoint = offset;
            _offsetRotationTween.EndRotation = rotation;
            _offsetScaleTween.EndScale = scale;

            PlayTween(_offsetTween);
            PlayTween(_offsetRotationTween);
            PlayTween(_offsetScaleTween);
        }

        private void PlayTween(ITween tween)
        {
            if (_offsetTweenComponent.IsPlaying(tween))
                _offsetTweenComponent.Stop(tween);

            _offsetTweenComponent.Play(tween);
        }
    }
}