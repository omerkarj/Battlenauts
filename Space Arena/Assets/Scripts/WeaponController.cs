using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public Transform model;
    public Avatar[] avatars;

    public WeaponStats stats;

    private WeaponStats[] weaponStatsArr;
    private Animator animator;

    enum Weapons { laserGun, alienWeapon, rocketLauncher };

	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
        // start the game with a laser gun!
        stats = new LaserGun();    
	}
	
	// Update is called once per frame
	void Update () {
        // point the weapon at the mouse cursor
        //model.LookAt(new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z));
	}

    // Switch the current weapon
    private void SwitchWeapon(Weapons weapon)
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
        animator.avatar = avatars[(int) weapon];
    }
}
