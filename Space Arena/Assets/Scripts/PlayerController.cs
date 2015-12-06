using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private int hp;
    private Text hpText;
    private WeaponController weaponController;

	// Use this for initialization
	void Start () {
        hp = 100;
        hpText = GameObject.FindGameObjectWithTag("HPText").GetComponent<Text>();
        weaponController = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponController>();
	}
	
	// Update is called once per frame
	void Update () {
        // update HP in HUD
        hpText.text = "HP: " + hp;
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
            // Health pickup
            case "HealthDrop":
                hp += 10;
                break;
            // Hit by enemy shot
            case "EnemyShot":
                hp -= 10;
                break;
            // Collision with enemy / asteroid
            case "EnemyKamikaze":
            case "Asteroid":
                hp -= 20;
                break;
        }
        Destroy(other.gameObject);
    }
}
