using UnityEngine;
using System.Collections;
using System;

public class EnemySpawner : MonoBehaviour {

    public GameObject Minion;
    public Vector3 spawnLocation=new Vector3(-15,0,20);
    public float spawnInterval=7;
    private Vector3[] spawnSpots = new Vector3[9];
    bool notRunning = true;
    // Use this for initialization
    void Start () {

    }
	void Update()
    {
        if (notRunning)
        {
            notRunning = false;
            StartCoroutine(Spawning());
        }
    }

    private IEnumerator Spawning()
    {
        Spawn();
        yield return new WaitForSeconds(spawnInterval);
        notRunning = true;
    }

    // Update is called once per frame

    void Spawn()
    {


        Instantiate(Minion, spawnLocation, new Quaternion());
    }
}
