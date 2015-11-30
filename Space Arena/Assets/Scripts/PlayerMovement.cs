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
    private WeaponStats weapon;

    enum Facing { left, right };
    private Facing facing = Facing.right;

	// Use this for initialization
	void Start () {
	    rb = GetComponent<Rigidbody>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponController>().stats;
	}
	
	// Update is called once per frame
    void Update() {
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
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

        // Trigger weapon backfire
        if (Input.GetMouseButtonDown(0)) {
            Vector3 dir = -(new Vector3(mousePosition.x, mousePosition.y, mousePosition.z)
                - new Vector3(transform.position.x, transform.position.y, transform.position.z)).normalized;
            if (facing == Facing.left)
                // torque to the right
                SpeedBurst(dir * speed * weapon.force, -turnSpeed * weapon.force);
            else
                // torque to the left
                SpeedBurst(dir * speed * weapon.force, turnSpeed * weapon.force);
        }

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
