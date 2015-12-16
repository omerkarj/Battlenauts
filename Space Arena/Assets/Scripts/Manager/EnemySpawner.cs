using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject Minion;
    public float SpawnInterval = 7f;
    public Transform[] spawnPoints;


    private float spawnInterval;
    private Vector3[] spawnSpots = new Vector3[9];

    // Use this for initialization
    void Start () {

        spawnInterval = SpawnInterval;
        InvokeRepeating("Spawn", spawnInterval, spawnInterval);
    }
	
	// Update is called once per frame

    void Spawn()
    {

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(Minion, transform.position, new Quaternion());
    }
}
