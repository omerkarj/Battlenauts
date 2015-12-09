using UnityEngine;
using System.Collections;

public class random_velocity : MonoBehaviour {
	public int minRange;
	public int maxRange;
	// Use this for initialization
	void Start () {
		Rigidbody r = gameObject.GetComponent<Rigidbody> ();
		r.velocity = new Vector3 (Random.Range(minRange,maxRange), Random.Range(minRange,maxRange), Random.Range(minRange,maxRange));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
