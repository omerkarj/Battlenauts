using UnityEngine;
using System.Collections;

public class earthScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    float rotationsPerMinute = 1;
	void Update () {

        transform.Rotate(new Vector3(0, 6.0F * rotationsPerMinute * Time.deltaTime, 0));
    }
}
