namespace Game.Entities.Cards
{
    public interface ICard
    {
        event System.Action StateChanged;

        //Common for all types of cards
        Player Owner { get; }
        int ManaCost { get; }

        CardVisuals VisualStats { get; }
        IEffectProvider CardEffect { get; }

        IMovement CardMovements { get; }
        ICardAnimations CardAnimations { get; }
        IHighlight CardHighlight { get; }

        //Each card have one effect. Or more?
        //Each card have effects of interaction (From deck, pick, play on table)
        //Do it with states? Use state when picked? Or just change animation and handle movement outside?

        //Make card provider with inbuilt card effects?

        //Unit cards also have hp and attack

        //Additional info: Name, portrait, description
        //Effect descriptions should be taken from effects themselves and put on top
    }
}