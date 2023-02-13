using UnityEngine;

public class BasicGun : MonoBehaviour
{
    //TODO: Use currentWeapon from the inventory class rather than this class

    public PlayerInventoryData currentInventory;
    //public FPSGunData currentWeapon;

    public Animator currentWeaponAnimator;

    

    private RaycastHit hit;
    private bool isAiming = false;

    //For fire rate checks and recoil cooldown
    private float timeSinceLastAttack;
    
    //Uncomment and use the below variable if you don't want to use the fire rate as the cooldown
    public float recoilCooldownCutoff = 0.3f;

    private void Start()
    {
        currentInventory.Initiate();
    }

    private void Update()
    {
        //Attacking
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        else
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        //Aiming
        if (Input.GetMouseButton(1))
        {
            //AIM
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }

        //Reloading
        if (currentInventory.currentWeapon.usesAmmo && Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        //Recoil cooldown
        if (timeSinceLastAttack > recoilCooldownCutoff && 
            currentInventory.currentWeapon.currentAccuracyAngle > currentInventory.currentWeapon.baseAccuracyAngle)
        {
            currentInventory.currentWeapon.currentAccuracyAngle -= currentInventory.currentWeapon.accuracyRecoverIncrement * Time.deltaTime;
            //Reset back to base accuracy angle if we overshoot the subtraction with the above line.
            if (currentInventory.currentWeapon.currentAccuracyAngle < currentInventory.currentWeapon.baseAccuracyAngle)
                currentInventory.currentWeapon.currentAccuracyAngle = currentInventory.currentWeapon.baseAccuracyAngle;
        }
    }

    private void Attack()
    {

        if(currentInventory.currentWeapon.Attack())
        {
            Vector3 shootDirection = transform.forward;

            //If the weapon is not perfectly accurate, we create a random direction within the accuracy cone to use
            if (currentInventory.currentWeapon.currentAccuracyAngle > 0)
            {
                shootDirection = GetRecoilDirection(transform.forward, currentInventory.currentWeapon.currentAccuracyAngle);
            }

            if (currentInventory.currentWeapon.projectileCount > 1)
            {
                //Temporary direction to track each projectile's direction in a pattern
                Vector3 patternDirection;

                //This loop creates the pattern
                for(int i = 0; i < currentInventory.currentWeapon.projectileCount; i++)
                {
                    patternDirection = GetRecoilDirection(shootDirection, currentInventory.currentWeapon.patternAccuracyAngle);
                    RaycastAttack(patternDirection);
                }
            }
            else
            {
                RaycastAttack(shootDirection);
            }
            
            //For weapons with variable accuracy, increment the accuracy angle towards the max accuracy angle upon a successful hip fire attack.
            if(!isAiming && currentInventory.currentWeapon.baseAccuracyAngle > 0)
            {
                currentInventory.currentWeapon.currentAccuracyAngle += currentInventory.currentWeapon.accuracyDiminishIncrement;

                //If we go over the max accuracy angle with the above line, then set it back to the max accuracy angle
                if (currentInventory.currentWeapon.currentAccuracyAngle > currentInventory.currentWeapon.maxAccuracyAngle)
                    currentInventory.currentWeapon.currentAccuracyAngle = currentInventory.currentWeapon.maxAccuracyAngle;
                    
            }

            timeSinceLastAttack = 0;
        }
    }

    public void RaycastAttack(Vector3 direction)
    {
             
        if (Physics.Raycast(transform.position, direction, out hit, currentInventory.currentWeapon.maxRange))
        {
            Debug.DrawLine(transform.position, hit.point);

            float armorMult = (hit.transform.GetComponent<KillableObject>() != null) ? hit.transform.GetComponent<KillableObject>().armorMult : 1f;
            int hitDamage = currentInventory.currentWeapon.CalculateDamage(hit.distance, armorMult);

            HitData hitData = new HitData(hitDamage, this.transform.position, hit, currentInventory.currentWeapon.defaultImpactPrefab, currentInventory.currentWeapon.impactSize);

            hit.transform.SendMessage("TakeDamage", hitData, SendMessageOptions.DontRequireReceiver);
        }
    }

    public Vector3 GetRecoilDirection(Vector3 originalDirection, float angle)
    {
        Vector3 recoilDirection = originalDirection + new Vector3(
                    Random.Range(
                        -angle,
                        angle
                        ),
                    Random.Range(
                        -angle,
                        angle
                        ),
                    Random.Range(
                        -angle,
                        angle
                        )
                    );

        //recoilDirection.Normalize();
        return recoilDirection;
    }

    private void Reload()
    {
        currentInventory.currentWeapon.Reload();     
    }
}
