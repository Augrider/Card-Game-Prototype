using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entities.Tables;
using Game.Entities.Units;
using UnityEngine;

namespace Game.Controller
{
    public class GameRules : MonoBehaviour
    {
        private IUnit _playerOne;
        private IUnit _playerTwo;

        [SerializeField] private TurnStateController _turnState;


        //Subscribe to hero state changes, stop game when someone dead

        IEnumerator Start()
        {
            yield return null;

            var _playerOne = Table.GetTableSide(Player.One).HeroUnit;
            var _playerTwo = Table.GetTableSide(Player.Two).HeroUnit;

            _playerOne.StateChanged += OnHeroStateChanged;
            _playerTwo.StateChanged += OnHeroStateChanged;
        }

        private void OnEnable()
        {
            GameState.GameState.PlayerChanged += OnPlayerChanged;

            // if (_playerOne == null || _playerTwo == null)
            //     return;

            // _playerOne.StateChanged += OnHeroStateChanged;
            // _playerTwo.StateChanged += OnHeroStateChanged;
        }

        private void OnDisable()
        {
            GameState.GameState.PlayerChanged -= OnPlayerChanged;

            // if (_playerOne == null || _playerTwo == null)
            //     return;

            // _playerOne.StateChanged -= OnHeroStateChanged;
            // _playerTwo.StateChanged -= OnHeroStateChanged;
        }



        private void OnPlayerChanged()
        {
            //Do something?
        }

        private void OnHeroStateChanged()
        {
            if (_playerOne.State.UnitHealth <= 0)
                StopGame(_playerOne.Owner);
            if (_playerTwo.State.UnitHealth <= 0)
                StopGame(_playerTwo.Owner);
        }

        private void StopGame(Player player)
        {
            _turnState.Stop();

            Debug.Log($"Player {(int)player} dead!");
        }
    }
}