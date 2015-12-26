using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float speed = 3000; 
    public float speedLimit = 3000;
    public float turnSpeed = 400;
    public float turnSpeedLimit = 400;
    public Transform model;
    public GameObject thrusterPrefab;
    public AudioClip deathAudio;

    private const float MIN_Y_ROTATION = 110;
    private const float MAX_Y_ROTATION = 250;

    private Rigidbody rb;
    private Transform thrusterPos;
    private Vector3 mousePosition;
    private float rotation;
    public bool isDead;
    public bool startEnterAnimation;
    public bool inEnterAnimation = false;

    private Animator astroAnimator;
    private AudioSource audioSource;

    public enum Facing { left, right };
    public Facing facing = Facing.right;

	// Use this for initialization
	void Start () {
	    rb = GetComponent<Rigidbody>();
        thrusterPos = GameObject.FindGameObjectWithTag("ThrusterPos").GetComponent<Transform>();
        isDead = false;

        astroAnimator = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("lastLevel") == 2)
            ResetPlayer();

        PlayerPrefs.SetInt("TimesPlayed", PlayerPrefs.GetInt("TimesPlayed") + 1);
	}
	
	// Update is called once per frame
    void Update() {
        if (startEnterAnimation) {
            if (!inEnterAnimation)
            {
                inEnterAnimation = true;
                rb.constraints = RigidbodyConstraints.None;
                facing = Facing.right;
                transform.position = GameObject.FindGameObjectWithTag("Spaceship").transform.position;
                iTween.MoveTo(gameObject, iTween.Hash(
                    "position", new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0f),
                    "oncomplete", "OnStartAnimComplete", 
                    "time", 4f));
            }
        }
        else if (!isDead) {
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
            FaceMousePosition();

            // Keep transform Y-Axis and X-Axis rotations at 0
            transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
            // Keep transform at 0 on Z-axis
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
            pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
    }
 
    void FixedUpdate() {
        if (!isDead && !startEnterAnimation) {
            // Trigger speed burst on movement key press
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                SpeedBurst(Vector3.left, 1, true);
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                SpeedBurst(Vector3.right, -1, true);
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                SpeedBurst(Vector3.up, 1, true);
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                SpeedBurst(Vector3.down, -1, true);

            // Trigger weapon backfire
            if (Input.GetMouseButtonDown(0))
            {
                GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
                Vector3 dir = -(new Vector3(mousePosition.x, mousePosition.y, mousePosition.z)
                    - new Vector3(transform.position.x, transform.position.y, transform.position.z)).normalized;
                float weaponForce = weapon.GetComponent<WeaponController>().stats.force;
                if (facing == Facing.left)
                    // torque to the right
                    SpeedBurst(dir * weaponForce, -weaponForce, false);
                else
                    // torque to the left
                    SpeedBurst(dir * weaponForce, weaponForce, false);
            }

            // Keep velocity under check
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speedLimit);
        }
	}

    // Adds force to the player in the appropriate direction
    void SpeedBurst(Vector3 input, float torque, bool triggerJet) {
        Vector3 move = input * speed * Time.deltaTime;

        if (rb.angularVelocity.z > turnSpeedLimit)
            torque = 0;

        if (move != Vector3.zero) {
            rb.AddForce(move);
            rb.AddRelativeTorque(new Vector3(0, 0, torque * turnSpeed * Time.deltaTime));
        }

        // position thruster to "push" in the provided direction
        if (triggerJet) {
            GameObject thruster = Instantiate(thrusterPrefab, thrusterPos.position, Quaternion.identity) as GameObject;
            thruster.gameObject.transform.LookAt(-input * 100);
            thruster.GetComponent<ParticleSystem>().Play();
            thruster.GetComponent<AudioSource>().Play();
            Destroy(thruster, 2f);
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

        // Point the player arm towards mouse cursor
        GameObject weaponArm = GameObject.FindGameObjectWithTag("PlayerWeaponArm");
        float mouseArmAngle = (Mathf.Atan2(
                mousePosition.y - weaponArm.transform.position.y,
                mousePosition.x - weaponArm.transform.position.x) - Mathf.PI / 2) * Mathf.Rad2Deg;
        float zDir = (facing == Facing.right) ? 1 : -1;
        float zDiff = (facing == Facing.right) ? 180 : 0;
        float weaponArmZAngle = (mouseArmAngle + 90) * zDir + zDiff;
        weaponArm.transform.eulerAngles = new Vector3(weaponArm.transform.eulerAngles.x, weaponArm.transform.eulerAngles.y, weaponArmZAngle);
        weaponArm.transform.localEulerAngles = new Vector3(13f, 85f, weaponArm.transform.localEulerAngles.z);
    }

    // Kills the astronaut
    public void KillPlayer() {
        isDead = true;
        rb.angularDrag = 0;
        rb.drag = 0;
        if (rb.velocity.magnitude < 5)
            rb.AddForce(new Vector3(Random.Range(5, 20), Random.Range(5, 20), Random.Range(5, 20)));
        
        GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
        weapon.transform.parent = null;
        float rand = Random.Range(0, 0.5f);
        weapon.GetComponent<Rigidbody>().velocity = rb.velocity * rand;
        weapon.GetComponent<WeaponController>().enabled = false;
        gameObject.GetComponent<PlayerHealth>().enabled = false;

        audioSource.clip = deathAudio;
        audioSource.Play();
    }

    // Resets the player (new game)
    public void ResetPlayer()
    {
        GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
        PlayerHealth playerHealth = gameObject.GetComponent<PlayerHealth>();
        playerHealth.enabled = true;

        weapon.GetComponent<Rigidbody>().velocity = Vector3.zero;
        weapon.GetComponent<WeaponController>().enabled = true;
        weapon.transform.SetParent(GameObject.FindGameObjectWithTag("PlayerModel").transform);
        playerHealth.currentHealth = playerHealth.maxHealth;
        isDead = false;
        startEnterAnimation = true;
    }

    void OnStartAnimComplete()
    {
        startEnterAnimation = false;
        inEnterAnimation = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }
}
