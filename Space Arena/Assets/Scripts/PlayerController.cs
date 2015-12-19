using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject spaceship;

    private WeaponController weaponController;
    
	// Use this for initialization
	void Start () {
        weaponController = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponController>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            spaceship.GetComponent<missileLauncher>().launchNumber = GameObject.FindGameObjectsWithTag("target").Length;
            Debug.Log(spaceship.GetComponent<missileLauncher>().launchNumber + " missiles launched!");
            spaceship.GetComponent<missileLauncher>().startLaunch = true;
        }
    }

    void OnTriggerEnter (Collider other) {
        switch (other.gameObject.tag) {
            // Weapon pickups
            case "WeaponDrop-LaserGun":
                weaponController.SwitchWeapon(WeaponController.Weapons.laserGun);
                break;
            case "WeaponDrop-AlienWeapon":
                weaponController.SwitchWeapon(WeaponController.Weapons.alienWeapon);
                break;
        }
        if (other.gameObject.tag != "PlayerShot" && other.gameObject.tag != "target")
            Destroy(other.gameObject);
    }
}
