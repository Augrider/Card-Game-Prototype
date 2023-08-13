using System.Collections;
using System.Collections.Generic;
using Game.Actions;
using UnityEngine;

namespace Game.Entities
{
    public interface ITargetableEffectProvider : IEffectProvider
    {
        new ITargetableAction GetAction(Player player);
        ITargetableAction GetReverseAction(Player player);
    }
}