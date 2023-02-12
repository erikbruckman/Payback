using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillableObject : ShootableObject
{
    public int health = 100;

    public int armorHealth = 0;
    public float armorMult = 1f;

    public override void TakeDamage(HitData hitData)
    {
       // if(impactOverride != null)
    }
}
