using Game.Entities.Tables;

namespace Game.Selection
{
    public abstract class SelectionState : ISelectionState
    {
        public static ISelectionStateControl StateControl { get; set; }
        public static ISelectionVisuals SelectionVisuals { get; set; }
        
        public static ITableSide CurrentTable { get; set; }

        public static event System.Action Cancelled;

        public abstract void OnStateEnter();
        public abstract void OnStateExit();


        protected static void Cancel() => Cancelled?.Invoke();
    }
}