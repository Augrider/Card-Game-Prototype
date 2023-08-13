using Game.Entities.Cards;
using UnityEngine;

namespace Game.Selection
{
    public interface ISelectionVisuals
    {
        void StartDrawingLineToPointer(Vector3 origin);
        void StopDrawingLine();

        void StartCardFollowingPointer(ICard card);
        void StopCardFollowing();
    }
}