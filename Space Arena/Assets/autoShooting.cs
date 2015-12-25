using UnityEngine;
using System.Collections;

public class autoShooting : MonoBehaviour {

    public GameObject weapon;
    public GameObject barrel;
    public GameObject projectile;

    private GameObject target;
    private int shootCounter = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("target");
        else
        {
            Aim();
            shootCounter++;
            if (shootCounter >= 40)
            {
                Shoot();
                shootCounter = Mathf.RoundToInt(Random.Range(0, 20));
            }
        }

        // position the weapon in the player's had and point it towards the mouse pointer
        Transform playerHand = GameObject.FindGameObjectWithTag("PlayerHand").transform;
        weapon.transform.position = new Vector3(playerHand.position.x + 0.1f, playerHand.position.y + 0.02f, playerHand.position.z - 0.3f);
        Transform playerWeaponArm = GameObject.FindGameObjectWithTag("PlayerWeaponArm").GetComponent<Transform>();
        weapon.transform.localEulerAngles = new Vector3(-playerWeaponArm.localEulerAngles.z + 180, 180, 0);
	}

    // Keeps the player facing the mouse cursor at all times
    void Aim()
    {
        Vector3 xDistance = target.transform.position - transform.position;
        float relativeMousePosition = Vector3.Dot(xDistance, transform.right.normalized);

        // Point the player arm towards mouse cursor
        GameObject weaponArm = GameObject.FindGameObjectWithTag("PlayerWeaponArm");
        float mouseArmAngle = (Mathf.Atan2(
                target.transform.position.y - weaponArm.transform.position.y,
                target.transform.position.x - weaponArm.transform.position.x) - Mathf.PI / 2) * Mathf.Rad2Deg;
        float weaponArmZAngle = -(mouseArmAngle + 90);
        //weaponArm.transform.eulerAngles = new Vector3(weaponArm.transform.eulerAngles.x, weaponArm.transform.eulerAngles.y, weaponArmZAngle);
        //weaponArm.transform.localEulerAngles = new Vector3(13f, 85f, weaponArm.transform.localEulerAngles.z);

        weaponArm.transform.LookAt(target.transform);
    }


    void Shoot()
    {
        // get the barrel position of the current weapon
        Rigidbody clone;
        Vector3 clonePos = new Vector3(barrel.transform.position.x, barrel.transform.position.y, 0);
        Vector3 dir = (target.transform.position - clonePos).normalized;

        float angle = (Mathf.Atan2(
            target.transform.position.y - transform.position.y,
            target.transform.position.x - transform.position.x) - Mathf.PI / 2) * Mathf.Rad2Deg;

        clone = Instantiate(projectile, clonePos, Quaternion.Euler(new Vector3(0f, 0f, angle))) as Rigidbody;
        clone.velocity = dir * 12;
        clone.angularVelocity = Vector3.zero;

        Destroy(clone, 3f);
    }
}
