using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FPSGunData.asset", menuName = "FPS Item Data/FPS Gun Data"), System.Serializable]
public class FPSGunData : FPSWeaponData

{
    
    //The amount of raycast bullets fired per shot
    public int projectileCount = 1;

    //The maximum range that the ray will cast
    public float maxRange = 500f;

    //The range at which the bullet will inflict full damage
    public float maxDamageRange = 30f;

    //The range at which the bullet will inflict it's minimum damage
    public float minDamageRange = 100f;

    //The minimum damage the bullet will make after traveling to the min range, expressed as a multiplier of the base damage
    public float minDamageFactor = 0.75f;

    //The type of magazine that the gun can use
    public MagazineType magazineType = MagazineType.Glock;

    //Data about the magazine currently being used in the gun, including the ammo within the magazine
    public MagazineData currentMagazine;

    //Data about the ammo currently in the chamber
    public AmmoData chamberAmmo;

    //EFFECTS

    public GameObject muzzleEffect;

    public GameObject brass;

    public void Attack()
    {
        //int damage = CalculateDamage(distance, armorMult);

        if(currentMagazine.currentRounds > 0)
        {
            chamberAmmo = currentMagazine.currentAmmo;
            currentMagazine.currentRounds--;
        }
        else
        {
            chamberAmmo = null;
        }

        //return damage;
        //LoggingService.Log("Total Damage: " + damage);
    }

    public int CalculateDamage(float distance, float armorMult)
    {
        float finalDamage = baseDamage;

        //Velocity bonus calculations
        finalDamage *= chamberAmmo.velocityDamageMultiplier;

        //Range calculations
        if(distance > maxDamageRange)
        {
            if (distance > minDamageRange)
                finalDamage *= minDamageFactor;
            else
                finalDamage *= (minDamageFactor + ((distance - maxDamageRange) / (minDamageRange - maxDamageRange) * (1 - minDamageFactor)));

            LoggingService.Log("Distance: " + distance + ", Ranged Damage Calculation: " + finalDamage);
        }

        //Armor & Ammo Decision Tree calculations
        if(armorMult < 1)
        {
            if(chamberAmmo.isArmorPiercing)
            {
                finalDamage *= chamberAmmo.armorPiercingMultiplier * armorMult;

                if (chamberAmmo.isHollowPoint)
                    finalDamage *= chamberAmmo.hollowPointMultiplier;
            }
            else
            {
                finalDamage *= armorMult;
            }
        }
        else
        {
            if (chamberAmmo.isHollowPoint)
                finalDamage *= chamberAmmo.hollowPointMultiplier;
        }

        return (int)finalDamage;
    }

    //Leaving virtual so that you can override for weapons like a tube-fed shotgun
    public virtual void Reload(MagazineData newMagazine)
    {
        if(chamberAmmo == null && newMagazine.currentRounds > 0)
        {
            chamberAmmo = newMagazine.currentAmmo;
            newMagazine.currentRounds--;
        }
        currentMagazine = newMagazine;
    }

}
