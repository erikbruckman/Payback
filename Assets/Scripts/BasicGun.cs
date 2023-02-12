using UnityEngine;

public class BasicGun : MonoBehaviour
{
    //TODO: Use currentWeapon from the inventory class rather than this class

    public PlayerInventoryData currentInventory;
    //public FPSGunData currentWeapon;

    
    private float impactOffset = 0.01f;
    private Vector3 impactPosition;
    private Transform impact;
    private RaycastHit hit;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void Attack()
    {
        switch (currentInventory.currentWeapon.attackType)
        {
            case AttackType.RaycastGun:

                break;
            default:
                LoggingService.Log("You attacked but there is no active attack type!");
        }

        if(currentInventory.currentWeapon.chamberAmmo != null)
        {
            

            

            
        }
        LoggingService.Log("Click! You're out of Ammo");

        
    }

    private void RaycastAttack()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, currentWeapon.maxRange))
        {
            //Determine offsets
            impactPosition.x = transform.position.x > hit.point.x ? (hit.point.x + impactOffset) : (hit.point.x - impactOffset);
            impactPosition.y = transform.position.y > hit.point.y ? (hit.point.y + impactOffset) : (hit.point.y - impactOffset);
            impactPosition.z = transform.position.z > hit.point.z ? (hit.point.z + impactOffset) : (hit.point.z - impactOffset);

            impact = hit.transform; //THIS MAY CAUSE ISSUES WITH REFERENCES
            impact.position = impactPosition;

            //GameObject hitEffect = Instantiate(impactPrefab, impact, Quaternion.LookRotation(hit.normal));
            //hitEffect.transform.parent = hit.transform;
            //hitEffect.transform.localScale = new Vector3(currentWeapon.caliber, currentWeapon.caliber, currentWeapon.caliber);

            float armorMult = (hit.transform.GetComponent<KillableObject>() != null) ? hit.transform.GetComponent<KillableObject>().armorMult : 1f;
            int hitDamage = currentInventory.currentWeapon.CalculateDamage(hit.distance, armorMult);

            HitData hitData = new HitData(hitDamage, impact, currentWeapon.defaultImpactPrefab, currentWeapon.impactSize);

            hit.transform.SendMessage("TakeDamage", hitData, SendMessageOptions.DontRequireReceiver);

            //Call Attack() for weapon data (i.e. ammo) management.
            currentWeapon.Attack();
        }
    }

    private void Reload()
    {
        if(currentWeapon.GetType().Equals("FPSGunData")) //THIS WILL PROBABLY GIVE YOU PROBLEMS DOWN THE LINE
        {
            if (currentWeapon.currentMagazine.currentRounds < currentWeapon.currentMagazine.capacity && currentInventory.HasMagazine(currentWeapon.magazineType))
            {
                currentWeapon.Reload(currentInventory.UseMagazine(currentWeapon.magazineType));
            }
        }
        
    }
}
