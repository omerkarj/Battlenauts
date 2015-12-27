using UnityEngine;
using System.Collections;

public class Enemy2Spawner : MonoBehaviour {

    public GameObject Minion;
    public Vector3 spawnLocationLeft = new Vector3(9, 4, 0);
    public Vector3 spawnLocationRight = new Vector3(-8, 4, 0);
    public bool left = true;
    public float spawnInterval = 15;
    private Vector3[] spawnSpots = new Vector3[9];
    bool notRunning = true;
    private int healthPack = 3;
    public int EnemiesOnScreen=0;
    // Use this for initialization
    void Start()
    {

    }
    void Update()
    {
        if (notRunning)
            StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        notRunning = false;
        Spawn();
        yield return new WaitForSeconds(spawnInterval);
        notRunning = true;
    }

    // Update is called once per frame

    void Spawn()
    {
        GameObject minion;
        if (EnemiesOnScreen < 2)
        {
            if (left)
            {
                minion = Instantiate(Minion, spawnLocationLeft, Quaternion.Euler(0, -90, 0)) as GameObject;
                left = false;
                EnemiesOnScreen++;
            }
            else
            {
                minion = Instantiate(Minion, spawnLocationRight, Quaternion.Euler(0, 90, 0)) as GameObject;
                left = true;
                EnemiesOnScreen++;
            }
            healthPack--;

            if (healthPack <= 0)
            {
                minion.GetComponent<Enemy2Movement>().healthPacks = Mathf.RoundToInt(Random.Range(1, 5));
                Debug.Log("Health packs: " + minion.GetComponent<Enemy2Movement>().healthPacks);
                healthPack = 3;
            }
            else
                minion.GetComponent<Enemy2Movement>().healthPacks = 0;
        }
    }
}
