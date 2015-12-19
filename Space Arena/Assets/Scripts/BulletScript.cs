using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float BulletForce;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * BulletForce);
        Destroy(gameObject, 7f);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider theCollision)
    {
        if(theCollision.gameObject.tag=="target"||theCollision.gameObject.tag=="targeted")
        {
            Destroy(gameObject);

        }
    }
}
