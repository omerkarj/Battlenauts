using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public WeaponStats stats;
    private Text weaponText;

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
    }
}
