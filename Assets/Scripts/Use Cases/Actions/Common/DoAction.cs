using System.Collections;
using System.Collections.Generic;
using Game.Entities.Tables;
using UnityEngine;

namespace Game.Actions
{
    /// <summary>
    /// Apply provided function
    /// </summary>
    public class DoAction : Action
    {
        private System.Action<ITableSide> _action;
        private ITableSide _tableSide;


        public DoAction(ITableSide tableSide, System.Action<ITableSide> action)
        {
            _action = action;
            _tableSide = tableSide;
        }


        public override void OnStackEnter()
        {
            _action.Invoke(_tableSide);

            StartCleanup();
        }
    }
}