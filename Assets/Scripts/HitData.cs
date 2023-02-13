using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitData
{
    public int damage;

    //public bool makeImpact;
    public Vector3 origin;
    public RaycastHit hit;
    public GameObject impactPrefab;
    public float caliber;
    

    public HitData (int damage, Vector3 origin, RaycastHit hit, GameObject impactPrefab, float caliber)
    {
        this.damage = damage;

        //this.makeImpact = makeImpact;
        this.origin = origin;
        this.hit = hit;
        this.impactPrefab = impactPrefab;
        this.caliber = caliber;
        
    }
}
