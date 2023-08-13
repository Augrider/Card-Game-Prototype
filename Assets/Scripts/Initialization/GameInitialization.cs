using Game.Components.Table;
using Game.Components.Targets;
using Game.Components.Unit;
using Game.Controller;
using Game.Entities.Cards;
using Game.Plugins.ObjectPool;
using Game.Turn;
using UnityEngine;

namespace Game.Initialization
{
    public class GameInitialization : MonoBehaviour
    {
        [SerializeField] private TurnStateController _turnStateController;

        [SerializeField] private PlayerCardsData _playerOneCards;
        [SerializeField] private PlayerCardsData _playerTwoCards;

        [SerializeField] private TableSide _playerOneTable;
        [SerializeField] private TableSide _playerTwoTable;

        [SerializeField] private UnitStats _playerUnitStats;


        private void Start()
        {
            TableInitialization();

            DefaultTurnState.DefaultTargetData = new DefaultTarget(TargetAlignment.Enemy, TargetType.All);

            _turnStateController.GoToSelectStartHand(Player.One);
        }



        private void TableInitialization()
        {
            SetTableParameters(_playerOneTable, _playerOneCards);
            SetTableParameters(_playerTwoTable, _playerTwoCards);
        }

        private void SetTableParameters(TableSide tableSide, PlayerCardsData playerCards)
        {
            var playerUnit = tableSide.GetPlayerUnit();
            var playerVisuals = playerUnit.GetComponent<UnitVisuals>();

            playerVisuals.SetUnitVisuals(playerCards.PlayerVisuals);
            playerUnit.State.ReplaceFrom(_playerUnitStats);

            foreach (ICardProvider cardProvider in playerCards.Cards)
            {
                var card = ObjectPoolLocator.Service.GetNewCard(cardProvider, tableSide.Player, tableSide.HeroUnitPlace.Position, tableSide.HeroUnitPlace.Rotation);
                tableSide.Deck.AddCards(card);
            }

            tableSide.Deck.Shuffle();
        }
    }
}