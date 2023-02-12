using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineData : FPSItemData
{
    //Uncomment the below line if you want to use an ID based system for different magazine models
    //public int id = 0;

    public MagazineType magazineType = MagazineType.Glock;

    public int capacity = 15;

    public AmmoData currentAmmo;

    public int currentRounds = 15;
}

public enum MagazineType
{
    Glock,
    AR,
    AK,
    AICS,
}