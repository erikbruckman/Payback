using UnityEngine;

public class BasicGun : MonoBehaviour
{
    public GameObject impactPrefab;
    public int magazineSize = 10;
    private int roundsLeft;
    private float shootRange = 100f;
    private float impactOffset = 0.01f;
    private Vector3 impact;
    private RaycastHit hit;

    private void Start() => roundsLeft = magazineSize;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && roundsLeft > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void Shoot()
    {
        roundsLeft--;
        Debug.Log("You shot. Current Ammo: " + roundsLeft);

        if (Physics.Raycast(transform.position, transform.forward, out hit, shootRange))
        {
            //Determine offsets
            impact.x = transform.position.x > hit.point.x ? (hit.point.x + impactOffset) : (hit.point.x - impactOffset);
            impact.y = transform.position.y > hit.point.y ? (hit.point.y + impactOffset) : (hit.point.y - impactOffset);
            impact.z = transform.position.z > hit.point.z ? (hit.point.z + impactOffset) : (hit.point.z - impactOffset);

            Instantiate(impactPrefab, impact, Quaternion.LookRotation(hit.normal)).transform.parent = hit.transform;
        }
    }

    private void Reload()
    {
        if(roundsLeft < magazineSize)
        {
            roundsLeft = magazineSize;
            Debug.Log("You reloaded. Current Ammo: " + roundsLeft);
        }
    }
}
