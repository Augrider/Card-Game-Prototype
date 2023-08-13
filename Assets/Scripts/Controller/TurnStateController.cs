using UnityEngine;
using Game.Turn;
using Game.Selection;

namespace Game.Controller
{
    public class TurnStateController : MonoBehaviour, ITurnStateControl
    {
        private ITurnState _defaultTurnState = new DefaultTurnState();
        private SelectStartHandState _selectStartHand = new SelectStartHandState();

        private ITurnState _currentState;

        [SerializeField] private Canvas _normalTurnsUI;


        private void Awake()
        {
            TurnState.TurnStateControl = this;
        }

        private void OnDestroy()
        {
            TurnState.TurnStateControl = null;
        }


        public void GoToSelectStartHand(Player player)
        {
            Game.GameState.GameState.SetPlayer(player);

            _selectStartHand.CurrentPlayer = player;
            _normalTurnsUI.enabled = false;

            ChangeState(_selectStartHand);
        }

        public void GoToNormalTurns()
        {
            _normalTurnsUI.enabled = true;

            GameState.GameState.SetPlayer(Player.One);
            ChangeState(_defaultTurnState);
        }

        public void Stop()
        {
            ChangeState(null);
        }


        public void SwapPlayer()
        {
            GameState.GameState.SwitchPlayer();

            ChangeState(_defaultTurnState);
        }



        private void ChangeState(ITurnState newState)
        {
            _currentState?.OnStateExit();
            newState?.OnStateEnter();

            _currentState = newState;
        }
    }
}