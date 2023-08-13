using Game.Entities.Cards;

namespace Game.Entities.Units
{
    public interface IUnitSpawnData
    {
        CardVisuals VisualStats { get; }
        UnitStats UnitStats { get; }

        IEffectProvider[] UnitPlacedEffects { get; }
        ITargetableEffectProvider[] UnitOnTableEffects { get; }
    }
}