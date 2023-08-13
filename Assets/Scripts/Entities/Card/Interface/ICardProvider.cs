using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities.Cards
{
    public interface ICardProvider
    {
        bool IsUnitCard { get; }

        void SetCardParameters(GameObject card, Player owner);
    }
}