using System;
using System.Collections;
using System.Collections.Generic;
using Game.Actions;
using UnityEngine;


namespace Game.Controller
{
    public class ActionStack : MonoBehaviour, IActionStack
    {
        private Stack<IAction> _actionStack = new Stack<IAction>();

        public event System.Action StackGotEmpty;

        public int Count => _actionStack.Count;


        // Start is called before the first frame update
        void Awake()
        {
            Actions.Action.ActionStack = this;
        }

        void OnDestroy()
        {
            Actions.Action.ActionStack = null;
        }


        public void CancelCurrent()
        {
            if (Count <= 0)
                return;

            var action = _actionStack.Pop();
            action.OnCancelled();

            Debug.LogWarning($"{action} cancelled, {Count} left");

            if (Count <= 0)
                StackGotEmpty?.Invoke();
        }

        public void FinishCurrent()
        {
            if (Count <= 0)
                return;

            var action = _actionStack.Pop();

            Debug.LogWarning($"{action} finished, {Count} left");
            if (Count <= 0)
                StackGotEmpty?.Invoke();

            action.OnStackExit();
        }

        public void PutOnStack(IAction action)
        {
            _actionStack.Push(action);
            Debug.LogWarning($"{action} put on stack, {Count} left");

            action.OnStackEnter();
        }
    }
}