using UnityEngine;
using System.Collections;

public class asteroidCreator : MonoBehaviour {

    // Use this for initialization
    public float spawnRange=10;
    public Transform astroid;
    public float delaySpawn = 0.5F;
    int summonDelay;
    Vector3 randomVect;
	void Start () {
        StartCoroutine(createAstroids());

	}

    private IEnumerator createAstroids()
    {
        Vector3 v = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 10);
        Instantiate(astroid, v, transform.rotation);
        yield return new WaitForSeconds(delaySpawn);
        StartCoroutine(createAstroids());
    }

    // Update is called once per frame
    void Update () {

    }
}
