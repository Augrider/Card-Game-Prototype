using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameState
{
    public static class GameState
    {
        public static event Action PlayerChanged;

        public static Player CurrentPlayer { get; set; }


        public static void SwitchPlayer()
        {
            SetPlayer(CurrentPlayer == Player.One ? Player.Two : Player.One);
        }

        public static void SetPlayer(Player value)
        {
            CurrentPlayer = value;
            PlayerChanged?.Invoke();
        }

        //Other game parameters and state
    }
}