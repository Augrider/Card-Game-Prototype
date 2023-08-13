using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Plugins.Coroutines
{
    public class CoroutinesObject : MonoBehaviour, ICoroutinesProvider
    {
        private void OnEnable()
        {
            CoroutinesLocator.Provide(this);
        }

        private void OnDisable()
        {
            CoroutinesLocator.Provide(null);
        }


        public Coroutine DoAfterDelay(Action action, float delay)
        {
            return StartCoroutine(WaitAndExecute(action, delay));
        }



        private IEnumerator WaitAndExecute(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);

            action?.Invoke();
        }
    }
}