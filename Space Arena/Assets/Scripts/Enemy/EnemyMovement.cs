using UnityEngine;
using System.Collections;
using System;

public class EnemyMovement : MonoBehaviour {

    // Push parameters
    public float MinForce = 40f;
    public float MaxForce = 100f;
    public float DirectionChangeInterval = 1f;
    public int healthCounter = 3;
    public int damageReward = 10;
    public int killReward = 50;
    //Particle Effects when hit and detroyed
    public Transform explosionParticles;
    public Transform hitParticles;

    private float directionChangeInterval;
    private float x;
    private float y;
    private bool start = false;
    //
    // Use this for initialization
    void Start ()
    {
        int pathNo = (int)UnityEngine.Random.Range(1f, 7f);
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("EnemyPath" + pathNo), "time", 10));
        directionChangeInterval = DirectionChangeInterval;
        StartCoroutine(waitTenSeconds());
       // Push();
	}

    private IEnumerator waitTenSeconds()
    {
        yield return new WaitForSeconds(10F);
        start = true;
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
        if (start)
        { 
            float force = UnityEngine.Random.Range(MinForce, MaxForce);
            x = UnityEngine.Random.Range(-1f, 1f);
            y = UnityEngine.Random.Range(-1f, 1f);

            GetComponent<Rigidbody>().AddForce(force * new Vector3(x, y, 0));
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "PlayerShot")
        {
            healthCounter--;
            Instantiate(hitParticles, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            addScore(damageReward);

        }

        // Check if enemy is dead
        if (healthCounter == 0 )
        {
            Destroy(gameObject);
            Instantiate(explosionParticles, other.transform.position, Quaternion.identity);
            addScore(killReward);
        }
        if (other.gameObject.tag == "Missile")
        {
            Destroy(gameObject);
        }
    }

    void addScore(int value)
    {
       PlayerScore ps= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>();
        ps.updateScore(value);
    }
}
