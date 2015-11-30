using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float speed = 1000; 
    public float speedLimit = 6;
    public float turnSpeed = 200;
    public float turnSpeedLimit = 4;
    public Transform model;

    private const float MIN_Y_ROTATION = 110;
    private const float MAX_Y_ROTATION = 250;

    private Rigidbody rb;
    private Vector3 mousePosition;
    private float rotation;

    enum Facing { left, right };
    private Facing facing = Facing.right;

	// Use this for initialization
	void Start () {
	    rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
    void Update() {
        FaceMousePosition();

        // Keep transform Y-Axis and X-Axis rotations at 0
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
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
	}

    // Adds force to the player in the appropriate direction
    void SpeedBurst(Vector3 input, float torque) {
        Vector3 move = input * Time.deltaTime;

        if (rb.angularVelocity.z > turnSpeedLimit)
            torque = 0;

        if (move != Vector3.zero) {
            rb.AddForce(move);
            rb.AddRelativeTorque(new Vector3(0, 0, torque * Time.deltaTime));
        }
    }

    // Keeps the player facing the mouse cursor at all times
    void FaceMousePosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
        Vector3 xDistance = mousePosition - transform.position;
        float relativeMousePosition = Vector3.Dot(xDistance, transform.right.normalized);

        // Decide which way to face the player according to mouse position
        if (relativeMousePosition <= 0) {
            facing = Facing.left;
        }
        else if (relativeMousePosition > 0) {
            facing = Facing.right;
        }

        // Ease the player rotation to face the mouse
        float currentYRotation = model.localEulerAngles.y;
        if (facing == Facing.right && currentYRotation > MIN_Y_ROTATION) {
            model.localEulerAngles = Vector3.Slerp(model.localEulerAngles, new Vector3(0, MIN_Y_ROTATION, 0), Time.deltaTime * 1.6f);    
        }
        else if (facing == Facing.left && currentYRotation < MAX_Y_ROTATION) {
            model.localEulerAngles = Vector3.Slerp(model.localEulerAngles, new Vector3(0, MAX_Y_ROTATION, 0), Time.deltaTime * 1.6f);    
        }
    }
}
