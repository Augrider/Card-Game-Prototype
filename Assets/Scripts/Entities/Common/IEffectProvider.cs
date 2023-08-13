using Game.Actions;

namespace Game.Entities
{
    public interface IEffectProvider
    {
        //This card effect provider class should just give effect action for anyone outside

        //Card effects create actions. They all should have the same interface. Because of the level, interface won't contain parameters with IO interfaces
        //Unit effects are not actions, they have their own interface. However, we can create actions for them
        //Input, despite being fairly complex, in the end should just add actions. Playing cards can be blocked by actions queue, but not necessary.
        //In short, input and queue are generally separated. If actions require input, they can use public interface and await correct input (or skip/revert)

        IAction GetAction(Player player);
    }
}