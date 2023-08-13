namespace Game.Actions
{
    /// <summary>
    /// Action interface for entities that control order of operations
    /// </summary>
    public interface IAction
    {
        //Interface should be simple, no outside references
        //Implementations can use IO defined elsewhere

        //Events for exit/cancel?
        event System.Action Finished;
        event System.Action Cancelled;

        void OnStackEnter();
        void OnStackExit();
        void OnCancelled();
    }
}