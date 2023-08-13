using Game.Entities.Tables;
using Game.Components.Card;
using UnityEngine;
using Game.Plugins.ObjectPool;
using Game.Controller;
using Game.Components.Targets;
using Game.Turn;

public class DebugInitialization : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TargetAlignment _defaultTargetAlignment;

    [SerializeField] private TurnStateController _turnState;

    [SerializeField] private CardProvider _card;
    [SerializeField] private int _amount;

    private ITableSide PlayerTable => Table.GetTableSide(_player);


    private void Awake()
    {
        DefaultTurnState.DefaultTargetData = new DefaultTarget(_defaultTargetAlignment, TargetType.All);
    }


    [ContextMenu("Add Cards To Deck")]
    public void AddCardsToDeck()
    {
        for (int i = 0; i < _amount; i++)
        {
            PlayerTable.Deck.AddCards(ObjectPoolLocator.Service.GetNewCard(_card, _player, PlayerTable.HeroUnitPlace.Position, PlayerTable.HeroUnitPlace.Rotation));
        }
    }

    [ContextMenu("Fill Hand from Deck")]
    public void FromDeckToHand()
    {
        var cardAmount = PlayerTable.Deck.CardAmount;

        for (int i = 0; i < cardAmount; i++)
        {
            if (PlayerTable.Deck.TryGetCard(out var card) && !PlayerTable.Hand.TryAddCard(card))
            {
                Debug.LogWarning($"Cards in deck {PlayerTable.Deck.CardAmount}");
                PlayerTable.Deck.AddCards(card);
                return;
            }
        }
    }

    [ContextMenu("Start Turn")]
    public void StartTurn()
    {
        AddCardsToDeck();
        FromDeckToHand();

        _turnState.GoToNormalTurns();
    }
}
