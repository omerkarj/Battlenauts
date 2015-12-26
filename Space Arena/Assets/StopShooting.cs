using UnityEngine;
using System.Collections;

public class StopShooting : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isDead)
        {
            GetComponent<EnemyAttack>().enabled = false;
        }
	}
}
