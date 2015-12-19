using UnityEngine;
using System.Collections;

public class earthScript : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
   
	void Update () {

        transform.Rotate(new Vector3(0, 6.0F * speed * Time.deltaTime, 0));
    }
}
