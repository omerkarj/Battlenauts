using UnityEngine;
using System.Collections;

public class asteroidController : MonoBehaviour {

    // Use this for initialization
    Rigidbody r;
    public float speed = 1;

	void Start () {
        r = gameObject.GetComponent<Rigidbody>();
        float randomSpawnLoation = Random.Range(-10, 10);
        r.velocity = (new Vector3(randomSpawnLoation, randomSpawnLoation, 10) - transform.position)*speed;
        r.angularVelocity=new Vector3(0.3F,0.3f,0.3f);
        float rand = Random.Range(0.2F, 1);
        transform.localScale = new Vector3(rand, rand, rand);
        StartCoroutine(delayDestroy());

    }

    private IEnumerator delayDestroy()
    {
        yield return new WaitForSeconds(10F);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
