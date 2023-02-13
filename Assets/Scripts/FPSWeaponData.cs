using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FPSWeaponData : FPSItemData
{
    //Basic weapon variables

    public SlotType slotType = SlotType.Sidearm;

    public int baseDamage = 30;

    public float attackRate = 0.12f;

    public GameObject defaultImpactPrefab;
    public float impactSize = 0.09f;

    //Gun variables

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

    //The base accuracy cone for the first shot in a string (this will be affected by the player's skill)
    public float baseAccuracyAngle = 0.001f;

    //The maximum accuracy angle for rapid strings of fire
    public float maxAccuracyAngle = 0.05f;

    //The accuracy angle for aimed fire
    public float aimedAccuracyAngle = 0.001f;

    //The current accuracy angle of the weapon
    public float currentAccuracyAngle = 10.0f;

    //ONLY FOR WEAPONS WITH MULTILPLE PROJECTILES - The spread of a multi-projectile shot. This is separate from accuracy.
    public float patternAccuracyAngle = 3f;

    //The amount of angle that gets added to the accuracy cone per each attack
    public float accuracyDiminishIncrement = 1f;

    //The amount of angle that gets subtracted from the accuracy cone after taking a break from attacking.
    public float accuracyRecoverIncrement = 0.05f;

    //The amount of vertical rotation to apply to the camera
    public float verticalCameraRecoil = 0.75f;

    //The amount of horizontal rotation to apply to the camera
    public float horizontalCameraRecoil = 0.3f;

    //Whether or not the weapon uses ammo to attack. Good for melee and debugging.
    public bool usesAmmo = true;

    //An array of all selectable fire rates (used for switching between semi, burst, full auto)
    public FireRate[] fireRates;

    //The currently selected fire rate for the weapon
    public FireRate currentFireRate;

    //The maximum amount of ammo in the gun's magazine
    public int magazineCapacity = 17;

    //The current amount of ammo in the gun's magazine
    public int currentMagazineAmmo = 0;

    //The current amount of spare ammo in your inventory
    public int reserveAmmo = 0;

    //EFFECTS

    public GameObject muzzleEffect;

    public GameObject brass;


    //Attack() returns true if the weapon can attack, false if the weapon cannot.
    public bool Attack()
    {
        //int damage = CalculateDamage(distance, armorMult);

        if (!usesAmmo || currentMagazineAmmo > 0)
        {
            if(usesAmmo)
                currentMagazineAmmo--;
            return true;
        }
        else
        {
            LoggingService.LogError("ERROR: Weapon that needs ammo cannot attack without ammo");
            return false;
        }

        //return damage;
        //LoggingService.Log("Total Damage: " + damage);
    }

    //This method is virtual so that you can override for weapons like a tube-fed shotgun
    public virtual void Reload()
    {
        if (usesAmmo && currentMagazineAmmo < magazineCapacity + 1 && reserveAmmo > 0)
        {
            bool plusOne = currentMagazineAmmo > 0;
            bool canLoadFullMag = currentMagazineAmmo >= magazineCapacity;
            int numToLoad = plusOne ? (magazineCapacity + 1) - currentMagazineAmmo : magazineCapacity - currentMagazineAmmo;

            if (reserveAmmo >= numToLoad)
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
        else
            LoggingService.Log("This weapon does not use ammo and has no need to reload!");
        //THIS LOG MESSAGE IS ALL-ENCOMPASSING FOR ALL RELOAD ISSUES RIGHT NOW. MORE CODE IS NEEDED FOR MORE PRECISE LOGS
    }

    public int CalculateDamage(float distance, float armorMult = 1)
    {
        float finalDamage = baseDamage;

        //Range calculations
        if (distance > maxDamageRange)
        {
            if (distance > minDamageRange)
                finalDamage *= minDamageFactor;
            else
                finalDamage *= (minDamageFactor + ((distance - maxDamageRange) / (minDamageRange - maxDamageRange) * (1 - minDamageFactor)));

            LoggingService.Log("Distance: " + distance + ", Ranged Damage Calculation: " + finalDamage);
        }

        return (int)finalDamage;
    }
}

//This enumerator will be used to tell the inventory code where it can put the weapon.
public enum SlotType
{
    Main,
    Sidearm,
    Melee
}

// SemiAuto is one attack per click, FullAuto is continuous clicks, SingleShot will require an animation to rechamber between each shot.
public enum FireRate
{
    SemiAuto,
    FullAuto,
    SingleShot
}