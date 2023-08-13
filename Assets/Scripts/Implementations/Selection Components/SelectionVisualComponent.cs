using System.Collections;
using System.Collections.Generic;
using Game.Entities.Cards;
using Game.Plugins.Input;
using Game.Selection;
using UnityEngine;

namespace Game.Components.Selection
{
    public class SelectionVisualComponent : MonoBehaviour, ISelectionVisuals
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private Vector3 _lineOffset;

        private Coroutine _drawLineProcess;
        private Coroutine _cardFollowProcess;


        void Awake()
        {
            SelectionState.SelectionVisuals = this;
        }

        void OnDestroy()
        {
            SelectionState.SelectionVisuals = null;
        }


        public void StartCardFollowingPointer(ICard card)
        {
            _cardFollowProcess = StartCoroutine(CardFollowProcess(card));
        }

        public void StartDrawingLineToPointer(Vector3 origin)
        {
            _drawLineProcess = StartCoroutine(DrawLineProcess(origin));
        }


        public void StopCardFollowing()
        {
            if (_cardFollowProcess != null)
                StopCoroutine(_cardFollowProcess);
        }

        public void StopDrawingLine()
        {
            _line.enabled = false;

            if (_drawLineProcess != null)
                StopCoroutine(_drawLineProcess);
        }



        private IEnumerator DrawLineProcess(Vector3 origin)
        {
            _line.enabled = true;
            _line.SetPosition(0, origin + _lineOffset);

            while (true)
            {
                _line.SetPosition(1, InputLocator.Service.PointerPosition + _lineOffset);

                yield return null;
            }
        }

        private IEnumerator CardFollowProcess(ICard card)
        {
            while (true)
            {
                card.CardMovements.Position = InputLocator.Service.PointerPosition;

                yield return null;
            }
        }
    }
}