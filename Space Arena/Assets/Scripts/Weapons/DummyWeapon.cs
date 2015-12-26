using UnityEngine;
using System.Collections;

public class DummyWeapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
