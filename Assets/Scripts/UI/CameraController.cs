using System;
using System.Collections;
using System.Collections.Generic;
using Developed.TweenSystem;
using Developed.TweenSystem.Tweens;
using UnityEngine;

namespace Game.Plugins.Input
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;

        [SerializeField] private RotationTween _rotationTween;
        [SerializeField] private TweenComponent _tweener;


        void OnEnable()
        {
            Game.GameState.GameState.PlayerChanged += RefreshCamera;
        }

        void OnDisable()
        {
            Game.GameState.GameState.PlayerChanged -= RefreshCamera;
        }



        private void RefreshCamera()
        {
            var cameraEuler = Vector3.zero;
            cameraEuler.z = Game.GameState.GameState.CurrentPlayer == Player.One ? 0 : 180;

            _rotationTween.StartRotation = _pivot.rotation;
            _rotationTween.EndRotation = Quaternion.Euler(cameraEuler);

            _tweener.Play(_rotationTween);
        }
    }
}