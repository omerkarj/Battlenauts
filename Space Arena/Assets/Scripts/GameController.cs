using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public List<GameObject> weaponPrefabs;

    private PlayerController playerController;
    private PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        InvokeRepeating("SpawnWeapon", 0, 5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (playerController.hp <= 0) {
            playerController.hp = 0;
            playerMovement.KillPlayer();
        }
	}

    private void SpawnWeapon()
    {
        Vector3 position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
        //This will spawn them at random position with (0,0,0) rotation
        Instantiate(weaponPrefabs[Random.Range(0, weaponPrefabs.Count)], position, Quaternion.identity);
    }
}
