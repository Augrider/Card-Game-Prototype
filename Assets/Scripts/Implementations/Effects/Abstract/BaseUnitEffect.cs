using Game.Actions;
using Game.Entities;
using Game.Entities.Tables;
using Game.Entities.Units;

namespace Game.Components.Effects
{
    //TODO: Spawn another unit effect, Target selection mode
    public abstract class BaseUnitEffect : ITargetableEffectProvider
    {
        public ITargetData TargetData { get; private set; }
        public bool SelectInsideAction { get; private set; }


        public BaseUnitEffect(ITargetData targetData, bool selectInsideAction)
        {
            TargetData = targetData;
            SelectInsideAction = selectInsideAction;
        }


        public abstract ITargetableAction GetAction(Player player);
        public abstract ITargetableAction GetReverseAction(Player player);

        IAction IEffectProvider.GetAction(Player player) => GetAction(player);
    }
}