using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSWeaponData : FPSItemData
{
    public SlotType slotType = SlotType.Sidearm;
    public AttackType attackType = AttackType.RaycastGun;

    public int baseDamage = 30;

    public float attackRate = 0.12f;

    public GameObject defaultImpactPrefab;
    public float impactSize = 0.09f;

    public virtual float CalculateDamage(float armorMult = 1)
    {
        return baseDamage * armorMult;
    }
}

//This enumerator will be used to tell the inventory code where it can put the weapon.
public enum SlotType
{
    Main,
    Sidearm,
    Melee
}

//This enumerator will be used to tell the combat code how to attack.
public enum AttackType
{
    RaycastGun,
    RaycastMelee,
    PhysicsGun,
    Throwable
}