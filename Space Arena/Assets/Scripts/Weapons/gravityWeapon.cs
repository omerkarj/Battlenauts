using UnityEngine;
using System.Collections;
using System;

public class gravityWeapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void onTriggerEnter(Collider theCollision)
    {
        if (theCollision.gameObject.tag == "target" || theCollision.gameObject.tag == "targeted")
        {
            theCollision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        }
    void OnTriggerStay(Collider theCollision)
    {
       if(theCollision.gameObject.tag=="target"|| theCollision.gameObject.tag == "targeted")
        {
            Debug.Log("gravityweapon");
            Rigidbody enemyRB = theCollision.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = gameObject.transform.position - theCollision.gameObject.transform.position;
            if (enemyRB != null)
            {
                enemyRB.AddForce(direction *5, ForceMode.Force);
            }
        }
      
    }

}
