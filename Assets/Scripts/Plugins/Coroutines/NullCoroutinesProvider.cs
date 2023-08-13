using System;
using System.Collections;
using UnityEngine;

namespace Game.Plugins.Coroutines
{
    internal class NullCoroutinesProvider : ICoroutinesProvider
    {
        public Coroutine DoAfterDelay(Action action, float delay)
        {
            throw new NotImplementedException();
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            throw new NotImplementedException();
        }


        public void StopCoroutine(Coroutine routine)
        {
            throw new NotImplementedException();
        }
    }
}