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

    //Whether or not the weapon uses ammo to attack. Good for melee and debugging.
    public bool usesAmmo = true;

    //The maximum amount of ammo in the gun's magazine
    public int magazineCapacity = 17;

    //The current amount of ammo in the gun's magazine
    public int currentMagazineAmmo = 0;

    //The current amount of spare ammo in your inventory
    public int reserveAmmo = 0;

    //EFFECTS

    public GameObject muzzleEffect;

    public GameObject brass;

    public void Attack()
    {
        //int damage = CalculateDamage(distance, armorMult);

        if(!usesAmmo || currentMagazineAmmo > 0)
        {
            currentMagazineAmmo--;
        }
        else
        {
            LoggingService.LogError("ERROR: Weapon that needs ammo cannot attack without ammo");
        }

        //return damage;
        //LoggingService.Log("Total Damage: " + damage);
    }

    public int CalculateDamage(float distance, float armorMult)
    {
        float finalDamage = baseDamage;

        //Range calculations
        if(distance > maxDamageRange)
        {
            if (distance > minDamageRange)
                finalDamage *= minDamageFactor;
            else
                finalDamage *= (minDamageFactor + ((distance - maxDamageRange) / (minDamageRange - maxDamageRange) * (1 - minDamageFactor)));

            LoggingService.Log("Distance: " + distance + ", Ranged Damage Calculation: " + finalDamage);
        }

        return (int)finalDamage;
    }

    //Leaving virtual so that you can override for weapons like a tube-fed shotgun
    public virtual void Reload()
    {
        bool plusOne = currentMagazineAmmo > 0;
        bool canLoadFullMag = currentMagazineAmmo >= magazineCapacity;
        int numToLoad = plusOne ? (magazineCapacity + 1) - currentMagazineAmmo : magazineCapacity - currentMagazineAmmo;

        if(reserveAmmo >= numToLoad)
        {
            if (plusOne)
                currentMagazineAmmo = magazineCapacity + 1;
            else
                currentMagazineAmmo = magazineCapacity;

            reserveAmmo -= numToLoad;
        }
        else
        {
            currentMagazineAmmo += numToLoad;
            reserveAmmo = 0;
        }
        
    }

}
