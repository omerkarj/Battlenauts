using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float speed; 
    public float turnSpeed;
    public float speedLimit;
    public float turnLimit;

    private Rigidbody rb;
    private Vector3 mousePosition;
    private float rotation;


	// Use this for initialization
	void Start () {
	    rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
    void Update() {

    }
 
    void FixedUpdate() {
        // Trigger speed burst on movement key press
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SpeedBurst(new Vector3(-speed, 0, 0), turnSpeed);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            SpeedBurst(new Vector3(speed, 0, 0), -turnSpeed);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            SpeedBurst(new Vector3(0, speed, 0), turnSpeed);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            SpeedBurst(new Vector3(0, -speed, 0), -turnSpeed);
        // Keep velocity under check
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, speedLimit);

        //Grab the current mouse position on the screen
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
        rotation = Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        rb.transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
        Debug.Log(rb.transform);
        Debug.Log(transform);
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
