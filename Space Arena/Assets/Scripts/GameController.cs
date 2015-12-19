using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public List<GameObject> weaponPrefabs;
    public GameObject playerPrefab;

    private PlayerController playerController;
    private PlayerMovement playerMovement;
    public int nextScoreLevel=500;
    private int scoreCounter;
	// Use this for initialization
	void Start () {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        scoreCounter = 1;
        InvokeRepeating("SpawnWeapon", 0, 5f);
	}
	
	// Update is called once per frame
	void Update () {
        increaseDiffuculty();
	}

    private void SpawnWeapon()
    {
        Vector3 position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
        //This will spawn them at random position with (0,0,0) rotation
        Instantiate(weaponPrefabs[Random.Range(0, weaponPrefabs.Count)], position, Quaternion.identity);
    }

    public void newPlayer()
    {
        playerMovement.gameObject.tag = "Untagged";
        Destroy(playerMovement.gameObject, 10f);

        GameObject clone = Instantiate(playerPrefab, Vector3.zero, Quaternion.Euler(new Vector3(0, 180, Random.Range(-20, 20)))) as GameObject;
        playerController = clone.GetComponent<PlayerController>();
        playerMovement = clone.GetComponent<PlayerMovement>();
        playerController.startEnterAnimation = true;
    }

    public void increaseDiffuculty()
    {
        PlayerScore ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>();
        int diffculty = ps.Score / scoreCounter;
        if (diffculty > nextScoreLevel)
        {
            Debug.Log("diffucly:"+ diffculty);
            //Increase here enemy spawn and difficulties
            scoreCounter++;
            nextScoreLevel += 50;
            Debug.Log("next Score Level:" + nextScoreLevel);
            reduceSpawnTime();
        }
    }

    private void reduceSpawnTime()
    {
        EnemySpawner es = gameObject.GetComponent<EnemySpawner>();
        if (es.spawnInterval > 2F)
        {
            es.spawnInterval--;
            Debug.Log("spawn interval: " + es.spawnInterval);
        }
        else
        {
            es.spawnInterval = es.spawnInterval * 0.90F;
            Debug.Log("spawn interval: " + es.spawnInterval);
        }
    }
}
