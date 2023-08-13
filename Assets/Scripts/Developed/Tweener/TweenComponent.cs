using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Developed.TweenSystem
{
    public sealed class TweenComponent : MonoBehaviour
    {
        [SerializeField] private TweenProvider _defaultTweenProvider;
        private ITween _defaultTween;

        private List<TweenProcess> _currentlyPlaying = new List<TweenProcess>();

        [SerializeField] private float _playbackSpeed = 1;

        public float PlaybackSpeed { get => _playbackSpeed; set => _playbackSpeed = value; }

        public event System.Action AllTweensFinishedPlaying;
        public event System.Action<ITween> TweenFinishedPlaying;


        void Awake()
        {
            if (_defaultTweenProvider != null)
                _defaultTween = _defaultTweenProvider.GetTween(gameObject);
        }


        /// <summary>
        /// Start playing
        /// </summary>
        public void PlayDefault(float startTime = 0, float delay = 0)
        {
            Play(_defaultTween, startTime, delay);
        }

        /// <summary>
        /// Start playing provided tween
        /// </summary>
        public void Play(ITween tween, float startTime = 0, float delay = 0)
        {
            if (IsPlaying(tween))
                return;

            // Debug.LogWarning($"Playing {tween}");

            var process = StartCoroutine(PlayTween(tween, tween.Parameters, startTime, delay));
            _currentlyPlaying.Add(new TweenProcess(tween, process));
        }


        public bool IsPlaying(ITween tween)
        {
            return _currentlyPlaying.Any(t => t.Tween == tween);
        }

        public bool IsPlaying()
        {
            return _currentlyPlaying.Count > 0;
        }


        /// <summary>
        /// Stop currently playing tween
        /// </summary>
        public void StopAll()
        {
            StopAllCoroutines();
            _currentlyPlaying.Clear();
        }

        /// <summary>
        /// Stop tween if playing, then notify if required
        /// </summary>
        /// <param name="notify">Should finished playing events be invoked?</param>
        public void Stop(ITween tween, bool notify = false)
        {
            if (!IsPlaying(tween))
                return;

            var record = GetTweenProcess(tween);

            StopCoroutine(record.Process);
            _currentlyPlaying.Remove(record);

            if (!notify)
                return;

            TweenFinishedPlaying?.Invoke(tween);

            if (_currentlyPlaying.Count <= 0)
                AllTweensFinishedPlaying?.Invoke();
        }



        private IEnumerator PlayTween(ITween tween, TweenParameters parameters, float startTime = 0, float delay = 0)
        {
            yield return new WaitForSeconds(delay);

            float time = startTime;

            while (!parameters.IsFixedTimeFinished(time))
            {
                time += Time.deltaTime * PlaybackSpeed;

                tween.ChangeState(parameters.GetLerpValue(time));
                yield return null;
            }

            Stop(tween, true);
        }


        private TweenProcess GetTweenProcess(ITween tween)
        {
            return _currentlyPlaying.FirstOrDefault(t => t.Tween == tween);
        }



        private struct TweenProcess
        {
            public ITween Tween;
            public Coroutine Process;


            public TweenProcess(ITween tween, Coroutine process)
            {
                Tween = tween;
                Process = process;
            }
        }
    }
}