using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public WeaponStats stats;
    public Transform[] projectile;
    public AudioClip[] weaponNames;
    public AudioClip weaponPickup;

    // UI Elements
    public Text weaponText;
    public Text ammo;
    private Weapons currentWeapon;
    private float nextFire;
    private AudioSource audioSource;

    public enum Weapons { laserGun, alienWeapon, gravityGun, dummyGun };

	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();

        // start the game with a laser gun!
        SwitchWeapon(Weapons.laserGun);  
	}

    void Update()
    {
        // position the weapon in the player's had and point it towards the mouse pointer
        Transform playerHand = GameObject.FindGameObjectWithTag("PlayerHand").transform;
        transform.position = new Vector3(playerHand.position.x + 0.1f, playerHand.position.y + 0.02f, playerHand.position.z - 0.3f);
        Transform playerWeaponArm = GameObject.FindGameObjectWithTag("PlayerWeaponArm").GetComponent<Transform>();
        transform.localEulerAngles = new Vector3(-playerWeaponArm.localEulerAngles.z + 180, 180, 0);
    }

	void FixedUpdate () {
        // update weapon name in HUD
        string weaponName = stats.name;
      
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

            Transform clone;
            Vector3 clonePos = new Vector3(barrel.position.x, barrel.position.y, 0);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
            Vector3 dir = (mousePos - clonePos).normalized;

            float angle = (Mathf.Atan2(
                mousePos.y - transform.position.y,
                mousePos.x - transform.position.x) - Mathf.PI / 2) * Mathf.Rad2Deg;
            clone = Instantiate(projectile[(int)currentWeapon], clonePos, Quaternion.Euler(new Vector3(0f, 0f, angle))) as Transform;
            clone.GetComponent<Rigidbody>().velocity = dir * stats.speed;
            clone.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
           
            Destroy(clone.gameObject, 12f);

            // reduce current ammo and fallback to laserGun if empty
            if (currentWeapon != Weapons.laserGun)
            {
                stats.ammo--;
                if (stats.ammo <= 0)
                    SwitchWeapon(Weapons.laserGun);
                // change ammo in HUD
                ammo.text = stats.ammo.ToString();
            }
        }
	}

    // Switch the current weapon
    public void SwitchWeapon(Weapons weapon)
    {
        switch (weapon) {
            case Weapons.laserGun:
                stats = new LaserGun();
                ammo.text = "∞";
                break;
            case Weapons.alienWeapon:
                stats = new AlienWeapon();
                break;
            case Weapons.gravityGun:
                stats = new GravityGun();
                break;
            case Weapons.dummyGun:
                stats = new DummyGun();
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
        // change weapon in HUD and play sound
        weaponText.text = stats.name;
        if (weapon != Weapons.laserGun)
        {
            ammo.text = stats.ammo.ToString();
        }
        StartCoroutine(PlayWeaponAudio());
    }

    IEnumerator PlayWeaponAudio()
    {
        audioSource.clip = weaponPickup;
        audioSource.Play();
        yield return new WaitForSeconds(weaponPickup.length);
        audioSource.clip = weaponNames[(int) currentWeapon];
        audioSource.Play();
    }
}
