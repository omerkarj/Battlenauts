using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    // Push parameters
    public float MinForce = 40f;
    public float MaxForce = 100f;
    public float DirectionChangeInterval = 1f;
    public int healthCounter = 3;


    private float directionChangeInterval;
    private float x;
    private float y;

    // Use this for initialization
    void Start ()
    {
        directionChangeInterval = DirectionChangeInterval;
        Push();
	}
	
	// Update is called once per frame
	void Update ()
    {
        directionChangeInterval -= Time.deltaTime;
        if (directionChangeInterval < 0)
        {
            Push();
            directionChangeInterval = DirectionChangeInterval;
        }
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);


    }

    void Push()
    {
        float force = Random.Range(MinForce, MaxForce);
        x = Random.Range(-1f, 1f);
        y = Random.Range(-1f, 1f);

        GetComponent<Rigidbody>().AddForce(force * new Vector3(x, y, 0));

    }

    void OnTriggerEnter (Collider other)
    {
        Debug.Log("Collided!!");
        if (other.gameObject.tag == "PlayerShot")
        {
            healthCounter--;
        }

        // Check if enemy is dead
        if (healthCounter == 0 || other.gameObject.tag == "Missile")
        {
            Destroy(gameObject);
        }
    }
}
