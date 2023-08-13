namespace Game.Actions
{
    /// <summary>
    /// Handles Actions stack
    /// </summary>
    public interface IActionStack
    {
        /// <summary>
        /// The amount of actions on stack
        /// </summary>
        int Count { get; }

        //Events for top cancellation and finish?
        event System.Action StackGotEmpty;

        /// <summary>
        /// Add action on top of the stack and play OnStackEnter
        /// </summary>
        void PutOnStack(IAction action);

        /// <summary>
        /// Remove action on top of the stack and play OnStackExit
        /// </summary>
        void FinishCurrent();

        /// <summary>
        /// Remove action on top of the stack and play OnCancelled
        /// </summary>
        void CancelCurrent();
    }
}