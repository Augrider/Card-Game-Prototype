using System.Collections.Generic;
using Game.Entities.Units;

namespace Game.Actions
{
    /// <summary>
    /// Action that targets only provided units and doesn't select them
    /// </summary>
    public interface ITargetableAction : IAction
    {
        ITargetData TargetData { get; }
        IUnit[] CurrentTargets { get; set; }

        bool SelectInsideAction { get; }
    }
}