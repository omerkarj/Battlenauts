using UnityEngine;
using System.Collections;

public class EnemyShield : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }
    void OnTriggerEnter(Collider theCollision)
    {
        if (theCollision.gameObject.tag == "PlayerShot") 
        {

            Destroy(theCollision.gameObject);
        }

    }


}
