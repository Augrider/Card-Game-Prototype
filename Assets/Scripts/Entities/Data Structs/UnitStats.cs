using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains unit parameters and some effects
/// </summary>
[System.Serializable]
public struct UnitStats
{
    public UnitType UnitType;

    public int UnitAttack;
    public int UnitMaxHealth;

    public bool Taunt;
    public bool Charge;
    //Poison, etc


    public UnitStats(UnitType unitType, int unitDamage, int unitHealth, bool taunt = false, bool charge = false)
    {
        UnitType = unitType;

        UnitAttack = unitDamage;
        UnitMaxHealth = unitHealth;

        Taunt = taunt;
        Charge = charge;
    }
}
