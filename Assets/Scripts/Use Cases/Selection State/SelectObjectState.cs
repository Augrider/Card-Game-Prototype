using Game.Entities.Cards;
using Game.Entities.Units;
using Game.Plugins.Input;

namespace Game.Selection
{
    public class SelectObjectState : SelectionState
    {
        public static event System.Action<ICard> CardSelected;
        public static event System.Action<IUnit> UnitSelected;


        public override void OnStateEnter()
        {
            //Enable tooltips (unit and card increase)
            InputLocator.Service.CardSelected += OnCardSelected;
            InputLocator.Service.UnitSelected += OnUnitSelected;
        }

        public override void OnStateExit()
        {
            //Disable tooltips (unit and card increase)
            InputLocator.Service.CardSelected -= OnCardSelected;
            InputLocator.Service.UnitSelected -= OnUnitSelected;
        }



        private void OnCardSelected(ICard card)
        {
            CardSelected?.Invoke(card);
        }

        private void OnUnitSelected(IUnit unit)
        {
            UnitSelected?.Invoke(unit);
        }
    }
}