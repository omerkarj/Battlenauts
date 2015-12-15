using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    private WeaponController weaponController;
    
	// Use this for initialization
	void Start () {
        weaponController = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponController>();
	}
	
	// Update is called once per frame
	void Update () {
        
       
	}

    void OnTriggerEnter (Collider other) {
        Debug.Log("touched: " + other.gameObject.tag);
        switch (other.gameObject.tag) {
            // Weapon pickups
            case "WeaponDrop-LaserGun":
                weaponController.SwitchWeapon(WeaponController.Weapons.laserGun);
                break;
            case "WeaponDrop-AlienWeapon":
                weaponController.SwitchWeapon(WeaponController.Weapons.alienWeapon);
                break;
        }
        Destroy(other.gameObject);
    }
}
