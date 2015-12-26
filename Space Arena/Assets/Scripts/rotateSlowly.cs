using UnityEngine;
using System.Collections;

public class rotateSlowly : MonoBehaviour {
    private Vector3 rotationVector;
    public float rotationSpeed = 0.2F;
	// Use this for initialization
	void Start () {
        rotationVector = gameObject.transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {

        rotationVector.x = (rotationVector.x + rotationSpeed * Time.deltaTime) % 360;
        gameObject.transform.eulerAngles = rotationVector;
    }
}
