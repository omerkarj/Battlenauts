using UnityEngine;
using System.Collections;

public class collisionDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision theCollision)
    {

		if ((theCollision.gameObject.tag == "target") || (theCollision.gameObject.tag == "targeted")) {

		} else {
			Destroy (gameObject);
		}
    }

}
