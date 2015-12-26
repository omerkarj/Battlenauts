using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject Minion;
    public Vector3 spawnLocation=new Vector3(-15,0,20);
    public float spawnInterval=7;
    public int LimitEnemiesOnScreen=6;
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
        int EnemiesOnScreen = GameObject.FindGameObjectsWithTag("target").Length;
        if (EnemiesOnScreen < LimitEnemiesOnScreen)
        {
            Spawn();
            
           
        }
        yield return new WaitForSeconds(spawnInterval);
        notRunning = true;
    }

    // Update is called once per frame

    void Spawn()
    {

        GameObject clone= Instantiate(Minion, spawnLocation, new Quaternion()) as GameObject;
        float r = Random.Range(-3F, -2F);
        clone.transform.localScale = new Vector3(r, r, r);
        clone.GetComponent<EnemyMovement>().healthCounter=(int) Random.Range(1,4);
    }
}
