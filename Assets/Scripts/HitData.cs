using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitData
{
    public int damage;

    //public bool makeImpact;
    public Transform transform;
    public GameObject impactPrefab;
    public float caliber;
    

    public HitData (int damage, Transform transform, GameObject impactPrefab, float caliber)
    {
        this.damage = damage;

        //this.makeImpact = makeImpact;
        this.transform = transform;
        this.impactPrefab = impactPrefab;
        this.caliber = caliber;
        
    }
}
