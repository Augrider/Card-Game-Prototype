namespace Game.Selection
{
    public interface ISelectionState
    {
        void OnStateEnter();
        void OnStateExit();
    }
}