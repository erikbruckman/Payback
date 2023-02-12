using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObject : MonoBehaviour
{
    public GameObject impactOverride;
    public bool makeImpactsOnHit;
    public virtual void TakeDamage(HitData hitData)
    {
        //Logic for taking damage

        if (makeImpactsOnHit)
            MakeImpact(hitData);
    }

    public virtual void MakeImpact(HitData hitData)
    {

    }
}
