using UnityEngine;
using System.Collections;

public class Enemy2Movement : MonoBehaviour {

    // Push parameters
    public float MinForce = 40f;
    public float MaxForce = 100f;
    public float DirectionChangeInterval = 1f;
    public int healthCounter = 6;
    public GameObject Child;


    private bool edgeDown = false;
    private bool edgeUp = false;
    private float directionChangeInterval;
    private float x;
    private float y;

    // Use this for initialization
    void Start () {
        directionChangeInterval = DirectionChangeInterval;    
        StartCoroutine(couroutineThatWaits());
        StartCoroutine(movementCoroutine());
    }

    IEnumerator movementCoroutine()
    {
        while (true)
        {
            iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("Enemy2Path"), "time", 5));
            yield return new WaitForSeconds(5f);
        }

    }
    IEnumerator couroutineThatWaits()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Child, transform.position + new Vector3(-1, -1, 0), new Quaternion());
            yield return new WaitForSeconds(3f);
        }
        
        

    }
        
	
	// Update is called once per frame
	void Update ()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //var dir = player.transform.position - transform.position;
        //transform.rotation = Quaternion.FromToRotation(transform.pos, dir);

        if (player != null)
        {
            //transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            transform.LookAt(player.transform.position);
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);


    }

    void Push()
    {
        float force = UnityEngine.Random.Range(MinForce, MaxForce);
        x = UnityEngine.Random.Range(-1f, 1f);
        y = UnityEngine.Random.Range(-1f, 1f);

        GetComponent<Rigidbody>().AddForce(force * new Vector3(x, y, 0));

    }

    void OnTriggerEnter(Collider other)
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
