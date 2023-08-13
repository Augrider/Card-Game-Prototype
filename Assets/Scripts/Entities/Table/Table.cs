using System;

namespace Game.Entities.Tables
{
    public static class Table
    {
        private static ITableSide _sideOne;
        private static ITableSide _sideTwo;


        public static ITableSide GetTableSide(Player player)
        {
            switch (player)
            {
                default:
                case Player.One:
                    return _sideOne;

                case Player.Two:
                    return _sideTwo;
            }
        }

        public static ITableSide GetTableSide(Player player, TargetAlignment alignment)
        {
            switch (alignment, player)
            {
                default:
                case (TargetAlignment.Friendly, Player.One):
                    return _sideOne;

                case (TargetAlignment.Friendly, Player.Two):
                    return _sideTwo;

                case (TargetAlignment.Enemy, Player.One):
                    return _sideTwo;

                case (TargetAlignment.Enemy, Player.Two):
                    return _sideOne;
            }
        }


        public static void Provide(ITableSide tableSide, Player player)
        {
            switch (player)
            {
                case Player.One:
                    _sideOne = tableSide;
                    break;

                case Player.Two:
                    _sideTwo = tableSide;
                    break;
            }
        }
    }
}