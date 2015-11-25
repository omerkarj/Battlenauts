using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float speed; 
    public float turnSpeed;
    public float speedLimit;
    public float turnLimit;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
	    rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
    void Update() {

    }
 
    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SpeedBurst(new Vector3(-speed, 0, 0), turnSpeed);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            SpeedBurst(new Vector3(speed, 0, 0), -turnSpeed);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            SpeedBurst(new Vector3(0, speed, 0), turnSpeed);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            SpeedBurst(new Vector3(0, -speed, 0), -turnSpeed);
        
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, speedLimit);
	}

    void SpeedBurst(Vector3 input, float torque) {
        Vector3 move = input * Time.deltaTime;

        if (rb.angularVelocity.z > turnLimit)
            torque = 0;

        if (move != Vector3.zero) {
            rb.AddForce(move);
            rb.AddRelativeTorque(new Vector3(0, 0, torque * Time.deltaTime));
        }
    }
}
