using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryData : ScriptableObject
{
    public FPSWeaponData primarySlot;
    public FPSWeaponData secondarySlot;

    public FPSWeaponData sidearmSlot;

    public FPSWeaponData meleeSlot;

    public List<FPSThrowableData> throwableWeapons;
    public int currentThrowable;

    //The currently equipped weapon
    public int currentSlot;
    public FPSWeaponData currentWeapon;

    public void PickupWeapon(FPSWeaponData newWeapon)
    {
        switch(newWeapon.slotType)
        {
            case SlotType.Main:

                if (primarySlot == null)
                    primarySlot = newWeapon;
                else if (secondarySlot == null)
                    secondarySlot = newWeapon;
                else if (currentWeapon == primarySlot)
                {
                    DropWeapon(1);
                    primarySlot = newWeapon;
                    EquipWeapon(1);
                }
                else if (currentWeapon == secondarySlot)
                {
                    DropWeapon(2);
                    secondarySlot = newWeapon;
                    EquipWeapon(2);
                }
                else
                {
                    DropWeapon(2);
                    secondarySlot = newWeapon;
                }
                break;

            case SlotType.Sidearm:

                if (sidearmSlot == null)
                    sidearmSlot = newWeapon;
                else if (secondarySlot == null)
                    secondarySlot = newWeapon;
                else if (primarySlot == null)
                    primarySlot = newWeapon;
                else if (currentWeapon = sidearmSlot)
                {
                    DropWeapon(3);
                    sidearmSlot = newWeapon;
                    EquipWeapon(3);
                }
                else
                {
                    DropWeapon(3);
                    sidearmSlot = newWeapon;
                }
                break;

            case SlotType.Melee:

                if (meleeSlot == null)
                    meleeSlot = newWeapon;
                else if (secondarySlot == null)
                    secondarySlot = newWeapon;
                else if (primarySlot == null)
                    primarySlot = newWeapon;
                else if (currentWeapon = meleeSlot)
                {
                    DropWeapon(4);
                    sidearmSlot = newWeapon;
                    EquipWeapon(4);
                }
                else
                {
                    DropWeapon(4);
                    meleeSlot = newWeapon;
                }
                break;
        }
    }

    public void EquipWeapon(int slot)
    {
        currentSlot = slot;
        currentWeapon = GetWeapon(currentSlot);
    }

    //FOCUS: This code will not work unless GetWeapon is returning pointers rather than new objects
    public void DropWeapon(int slot)
    {
        //Instantiate the weapon pickup
        if(currentSlot == slot)
        {
            currentWeapon = null;
            
            EquipWeapon(FindBestWeapon());
        }

        switch (slot)
        {
            case 1:
                this.primarySlot = null;
                break;
            case 2:
                this.secondarySlot = null;
                break;
            case 3:
                this.sidearmSlot = null;
                break;
            case 4:
                this.meleeSlot = null;
                break;
            case 5:
                throwableWeapons.RemoveAt(currentThrowable);
                if (currentThrowable == throwableWeapons.Count)
                {
                    currentThrowable--;
                }
                break;
            default:
                LoggingService.LogError("ERROR: Failure to return Weapon Data with slot index: " + slot);
                break;
        }
    }

    public FPSWeaponData GetWeapon(int slot)
    {
        switch(slot)
        {
            case 1:
                return this.primarySlot;
            case 2:
                return this.secondarySlot;
            case 3:
                return this.sidearmSlot;
            case 4:
                return this.meleeSlot;
            case 5:
                return this.throwableWeapons[currentThrowable];
            default:
                LoggingService.LogError("ERROR: Failure to return Weapon Data with slot index: " + slot);
                return null;
        }
    }

    public void RemoveMagazine(int index)
    {
        magazines.RemoveAt(index);
    }

    public int FindBestWeapon()
    {
        if (primarySlot != null)
            return 1;
        else if (secondarySlot != null)
            return 2;
        else if (sidearmSlot != null)
            return 3;
        else if (meleeSlot != null)
            return 4;
        else
        {
            LoggingService.LogError("ERROR: Could not find best weapon in inventory.");
            return -1;
        }
    }


}
