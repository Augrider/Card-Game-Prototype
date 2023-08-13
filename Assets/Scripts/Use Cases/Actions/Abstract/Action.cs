namespace Game.Actions
{
    public abstract class Action : IAction
    {
        public event System.Action Finished;
        public event System.Action Cancelled;

        public static IActionStack ActionStack { get; set; }

        public abstract void OnStackEnter();

        public virtual void OnStackExit() => Finished?.Invoke();
        public virtual void OnCancelled() => Cancelled?.Invoke();


        //Utility actions
        protected void StartCleanup()
        {
            var cleanup = new CleanDeadUnitsAction();

            cleanup.Finished += ActionStack.FinishCurrent;
            cleanup.Cancelled += ActionStack.FinishCurrent;

            ActionStack.PutOnStack(cleanup);
        }

        protected void StartApplyingTableEffects()
        {
            var tableEffectsAction = new ApplyTableEffectsAction();

            tableEffectsAction.Finished += ActionStack.FinishCurrent;
            tableEffectsAction.Cancelled += ActionStack.FinishCurrent;

            ActionStack.PutOnStack(tableEffectsAction);
        }
    }
}