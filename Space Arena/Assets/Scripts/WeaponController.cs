using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public WeaponStats stats;
    public Rigidbody[] projectile;

    private Text weaponText;
    private Weapons currentWeapon;
    private float nextFire;

    public enum Weapons { laserGun, alienWeapon, rocketLauncher };

	// Use this for initialization
	void Start () {
        //weaponText = GameObject.FindGameObjectWithTag("WeaponText").GetComponent<Text>();

        // start the game with a laser gun!
        SwitchWeapon(Weapons.laserGun);  
	}
	
	void FixedUpdate () {
        // update weapon name in HUD
        string weaponName = stats.name;
        //weaponText.text = "Current Weapon: " + weaponName;

        // Trigger weapon shot
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            nextFire = Time.time + stats.fireRate;

            // get the barrel position of the current weapon
            Transform barrel = transform;
            foreach (Transform tr in transform) {
                    if (tr.gameObject.activeInHierarchy == true && tr.tag == "Barrel")
                        barrel = tr.transform;
            }

            Rigidbody clone;
            Vector3 clonePos = new Vector3(barrel.position.x, barrel.position.y, 0);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
            Vector3 dir = (mousePos - clonePos).normalized;

            float angle = (Mathf.Atan2(
                mousePos.y - transform.position.y,
                mousePos.x - transform.position.x) - Mathf.PI / 2) * Mathf.Rad2Deg;

            clone = Instantiate(projectile[(int)currentWeapon], clonePos, Quaternion.Euler(new Vector3(0f, 0f, angle))) as Rigidbody;
            clone.velocity = dir * stats.speed;
            clone.angularVelocity = Vector3.zero;
           
            Destroy(clone, 3f);
        }
	}

    // Switch the current weapon
    public void SwitchWeapon(Weapons weapon)
    {
        switch (weapon) {
            case Weapons.laserGun:
                stats = new LaserGun();
                break;
            case Weapons.alienWeapon:
                stats = new AlienWeapon();
                break;
            case Weapons.rocketLauncher:
                stats = new RocketLauncher();
                break;
        }

        int index = 0;
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
            if (index == (int)weapon)
                child.gameObject.SetActive(true);
            index++;
        }

        currentWeapon = weapon;
    }
}
