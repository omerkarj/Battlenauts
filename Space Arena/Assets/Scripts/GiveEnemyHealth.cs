using UnityEngine;
using System.Collections;
using System;

public class GiveEnemyHealth : MonoBehaviour {

    public float healthInterval = 4f;


	// Use this for initialization
	void Start () {

	}


    // Update is called once per frame
    void Update () {
        healthInterval -= Time.deltaTime;
        if (healthInterval == 0)
        {
            healthInterval = 4f;
            giveHealthPack();
        }
	}

    private void giveHealthPack()
    {
        GameObject enemy2 = GameObject.Find("enemySpawner");

        if (enemy2 != null)
        {
            enemy2.GetComponent<Enemy2Movement>().healthPacks += 1;
        }
    }
}
