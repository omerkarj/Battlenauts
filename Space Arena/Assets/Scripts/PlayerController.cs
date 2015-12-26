using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject spaceship;
	public bool isPowerupOn = false;
    public AudioClip[] taunts;
    public float tauntWaitMin, tauntWaitMax;
    public AudioClip oxygenLevelCritical;
    private bool oxygenCrit = false;
    public AudioClip lowOxygenLevel;
    private bool lowOx = false;
    private WeaponController weaponController;
    private AudioSource audioSource;
    private float tauntWait;
    
	// Use this for initialization
	void Start () {
        weaponController = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponController>();
        audioSource = GetComponent<AudioSource>();
        tauntWait = Random.Range(tauntWaitMin, tauntWaitMax);
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<PlayerHealth>().currentHealth >= 45 && lowOx)
            lowOx = false;
        if (GetComponent<PlayerHealth>().currentHealth >= 25 && oxygenCrit)
            oxygenCrit = false;
        if (GetComponent<PlayerHealth>().currentHealth <= 40 && !lowOx)
        {
            audioSource.clip = lowOxygenLevel;
            audioSource.Play();
            lowOx = true;
        }
        if (GetComponent<PlayerHealth>().currentHealth <= 15 && !oxygenCrit)
        {
            audioSource.clip = oxygenLevelCritical;
            audioSource.Play();
            oxygenCrit = true;
        }
        // player taunts
        tauntWait -= Time.deltaTime;
        if (tauntWait <= 0)
        {
            audioSource.clip = taunts[Mathf.RoundToInt(Random.Range(0, taunts.Length - 1))];
            audioSource.Play();
            tauntWait = Random.Range(tauntWaitMin, tauntWaitMax);
        }
	}

    void FixedUpdate() {
		if(isPowerupOn){

        	if (Input.GetKeyDown(KeyCode.Space)) {
           	 spaceship.GetComponent<missileLauncher>().launchNumber = GameObject.FindGameObjectsWithTag("target").Length;
           	 Debug.Log(spaceship.GetComponent<missileLauncher>().launchNumber + " missiles launched!");
            	spaceship.GetComponent<missileLauncher>().startLaunch = true;
				isPowerupOn=false;
				GameObject player=GameObject.FindGameObjectWithTag("Player");
				player.GetComponent<PlayerScore>().powerUpCounter=0;
				player.GetComponent<SpecialPower>().PowerUp(0,player.GetComponent<PlayerScore>().pointsForPowerUp);
			}
        }
    }

    void OnTriggerEnter (Collider other) {
        Debug.Log("player collided with " + other.tag);
        switch (other.gameObject.tag) {
            // Weapon pickups
            case "WeaponDrop-LaserGun":
                weaponController.SwitchWeapon(WeaponController.Weapons.laserGun);
                break;
            case "WeaponDrop-AlienWeapon":
                weaponController.SwitchWeapon(WeaponController.Weapons.alienWeapon);
                break;
            case "WeaponDrop-GravityGun":
                weaponController.SwitchWeapon(WeaponController.Weapons.gravityGun);
                break;
            case "WeaponDrop-DummyGun":
                weaponController.SwitchWeapon(WeaponController.Weapons.dummyGun);
                break;
        }
        if (other.gameObject.tag != "PlayerShot" && other.gameObject.tag != "target")
            Destroy(other.gameObject);
    }
}
