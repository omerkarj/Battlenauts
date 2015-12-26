using UnityEngine;
using System.Collections;

public class RotateHealth : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Time.deltaTime, 5, 5);
    }
}
