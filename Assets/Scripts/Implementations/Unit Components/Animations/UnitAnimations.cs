using Developed.TweenSystem;
using Developed.TweenSystem.Tweens;
using UnityEngine;

namespace Game.Components.Unit
{
    public class UnitAnimations : BaseUnitAnimations
    {
        [SerializeField] private TweenComponent _tweenComponent;

        [SerializeField] private MoveTween _attackTween;
        [SerializeField] private RotationTween _damageTween;

        [SerializeField] private Vector3 _damageMaxDeviationEuler;

        public override bool IsIdle => !_tweenComponent.IsPlaying();
        //Animations for damage, death and attack


        void Awake()
        {
            _damageTween.EndRotation = Quaternion.Euler(_damageMaxDeviationEuler);
        }


        public override void OnPlaced()
        {
            Debug.LogWarning("Placed Animation");
            Unit.UnitHighlight.ToggleHide(false);
        }

        public override void OnDamageReceived()
        {
            Debug.LogWarning("Damage Animation");
            _tweenComponent.Play(_damageTween);
        }

        public override void OnDeath()
        {
            Debug.LogWarning("Death Animation");
            Unit.UnitHighlight.ToggleHide(true);
        }


        public override void PlayAttack(Vector3 position)
        {
            Debug.LogWarning($"Attack Animation");

            _attackTween.StartPoint = Unit.UnitMovements.Position;
            _attackTween.EndPoint = position;
            _tweenComponent.TweenFinishedPlaying += OnAttackFinishedPlaying;

            Unit.UnitHighlight.ToggleLift(true);
            _tweenComponent.Play(_attackTween);
        }



        private void OnAttackFinishedPlaying(ITween finished)
        {
            if (finished != _attackTween)
                return;

            InvokeAttackPerformed();

            _tweenComponent.TweenFinishedPlaying -= OnAttackFinishedPlaying;
        }
    }
}