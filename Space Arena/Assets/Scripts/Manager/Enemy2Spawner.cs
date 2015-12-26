using UnityEngine;
using System.Collections;

public class Enemy2Spawner : MonoBehaviour {

    public GameObject Minion;
    public Vector3 spawnLocation = new Vector3(11, 4, 0);
    public float spawnInterval = 15;
    private Vector3[] spawnSpots = new Vector3[9];
    bool notRunning = true;
    // Use this for initialization
    void Start()
    {

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


        Instantiate(Minion, spawnLocation, Quaternion.Euler(0, -90, 0));
    }
}
