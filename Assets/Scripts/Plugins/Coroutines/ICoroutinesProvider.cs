using System;
using System.Collections;
using UnityEngine;

namespace Game.Plugins.Coroutines
{
    public interface ICoroutinesProvider
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine routine);

        //Add others if needed
        Coroutine DoAfterDelay(Action action, float delay);
    }
}