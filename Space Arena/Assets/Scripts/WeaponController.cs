using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public WeaponStats stats;
    public Rigidbody[] projectile;

    private Text weaponText;
    private Weapons currentWeapon;

    public enum Weapons { laserGun, alienWeapon, rocketLauncher };

	// Use this for initialization
	void Start () {
        weaponText = GameObject.FindGameObjectWithTag("WeaponText").GetComponent<Text>();

        // start the game with a laser gun!
        SwitchWeapon(Weapons.laserGun);  
	}
	
	// Update is called once per frame
	void Update () {
        // update weapon name in HUD
        string weaponName = stats.name;
        weaponText.text = "Current Weapon: " + weaponName;

        // Trigger weapon shot
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody clone;
            Vector3 clonePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
            Vector3 dir = (mousePos - clonePos).normalized;

            clone = Instantiate(projectile[(int) currentWeapon], clonePos, Quaternion.identity) as Rigidbody;
            clone.transform.LookAt(mousePos);
            
            clone.velocity = dir * stats.speed;
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
