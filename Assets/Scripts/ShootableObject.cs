using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObject : MonoBehaviour
{
    public bool makeImpactsOnHit = true;

    public GameObject impactOverride;
    
    public float impactPositionOffset = 0.01f;

    public virtual void TakeDamage(HitData hitData)
    {
        //Logic for taking damage
        LoggingService.Log(name + " got hit!");
        if (makeImpactsOnHit)
            MakeImpact(hitData);
    }

    public virtual void MakeImpact(HitData hitData)
    {
        Vector3 impactPosition = hitData.hit.transform.position;

        impactPosition.x = hitData.origin.x > hitData.hit.point.x ? (hitData.hit.point.x + impactPositionOffset) : (hitData.hit.point.x - impactPositionOffset);
        impactPosition.y = hitData.origin.y > hitData.hit.point.y ? (hitData.hit.point.y + impactPositionOffset) : (hitData.hit.point.y - impactPositionOffset);
        impactPosition.z = hitData.origin.z > hitData.hit.point.z ? (hitData.hit.point.z + impactPositionOffset) : (hitData.hit.point.z - impactPositionOffset);

        if (impactOverride)
        {
            GameObject hitEffect = Instantiate(impactOverride, impactPosition, Quaternion.LookRotation(hitData.hit.normal));
            
            hitEffect.transform.localScale = new Vector3(hitData.caliber, hitData.caliber, hitData.caliber);
            hitEffect.transform.parent = this.transform;
        }
        else
        {
            GameObject hitEffect = Instantiate(hitData.impactPrefab, impactPosition, Quaternion.LookRotation(hitData.hit.normal));
            
            hitEffect.transform.localScale = new Vector3(hitData.caliber, hitData.caliber, hitData.caliber);
            hitEffect.transform.parent = this.transform;
        }       
    }
}
