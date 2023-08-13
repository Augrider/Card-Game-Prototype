using Game.Entities.Cards;
using Game.Entities.Tables;
using UnityEngine;

namespace Game.Actions
{
    //Cards can use different data for their effect

    //For example, unit cards require unit space, where unit will be placed
    //Cards that buff designated unit require you to select that unit
    //Cards in Hearthstone that give weapon or buff your hero just need to be played!

    //Where to decide, what selection operation to play and get results from?

    //First candidate: actions, since they usually define what data required
    //However, actions received after card is considered played, and no other way around

    //Second: Card effect provider. It can contain function to enable selection and receive result
    //But data still needs to be transferred to effect to make IAction, which means that effect provider should have state (is it bad?)

    //Theoretically, operations in playing card loop should be in this order:

    //Pressed on card (selecting object) -> Checking, what data needed for card (what needs to be selected) ->
    //Starting correct selection behavior (dragging card/selecting unit/selecting spot for unit card) ->
    //Either cancelled (returning card/stopping selection) or receiving selection result ->
    //Adding play card action with received selection results -> Waiting until all effects in queue are played -> Start selection process again

    public class PlayCardAction : Action
    {

        private ICard _card;
        private ITableSide _table;


        public PlayCardAction(Player player, ICard currentCard)
        {
            this._card = currentCard;
            this._table = Table.GetTableSide(player);
        }


        public override void OnStackEnter()
        {
            _table.Hand.TryTakeCard(_card);

            //Create action from card and put it on stack
            var cardAction = _card.CardEffect.GetAction(_table.Player);

            Debug.Log($"Card action {cardAction}");

            //Subscribe to top action cancel (cancel this and put card back)
            //Subscribe to top action finished ()
            cardAction.Finished += StartCleanup;
            cardAction.Cancelled += OnEffectCancelled;

            ActionStack.PutOnStack(cardAction);

            _card.CardHighlight.ToggleLift(true);
        }

        public override void OnStackExit()
        {
            //Played card successfully -> remove mana from pool, play card played animation
            Debug.Log("Card finished playing");
            _table.Mana -= _card.ManaCost;

            _table.Graveyard.AddCards(_card);
            _card.CardAnimations.OnCardPlayed();
            _card.CardHighlight.ToggleLift(false);
        }

        public override void OnCancelled()
        {
            //Failed to play card -> move card back to hand
            _table.Hand.TryAddCard(_card);
            _card.CardHighlight.ToggleLift(false);
        }



        private void OnEffectCancelled()
        {
            ActionStack.CancelCurrent();
        }
    }
}