using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoData : FPSItemData
{
    public float velocityDamageMultiplier = 1f;

    public bool isHollowPoint = false;
    public float hollowPointMultiplier = 2f;

    public bool isArmorPiercing = false;
    //Set this to 0 for complete armor negation
    public float armorPiercingMultiplier = 1f;
}
