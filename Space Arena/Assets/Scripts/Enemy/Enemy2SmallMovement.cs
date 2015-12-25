using UnityEngine;
using System.Collections;


public class Enemy2SmallMovement : MonoBehaviour
{


    public int killReward = 20;
    public Transform explosionParticles;

    private float forceInterval = 0.5f;
    private float forceTime = 3.5f;

    // Use this for initialization
    void Start()
    {
        
        
        StartCoroutine(couroutineThatWaits());
        

    }

    private IEnumerator couroutineThatWaits()
    {
        iTween.PunchPosition(gameObject, new Vector3(0, 0, Random.Range(1f, 15f)), 5f);
        var player = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(5f);
        iTween.MoveTo(gameObject, player.transform.position, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        forceInterval -= Time.deltaTime;
        forceTime -= Time.deltaTime;

        if (forceInterval == 0)
        {
            forceInterval = 0.5f;
            Vector3 direction = new Vector3(Random.Range(1f, 15f), 0, 0) - transform.position;
            gameObject.GetComponent<Rigidbody>().AddForce(direction * 5, ForceMode.Force);
        }

        if (forceTime == 0)
        {
            forceTime = 3.5f;

        }
        */

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "PlayerShot" || other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(explosionParticles, other.transform.position, Quaternion.identity);
            addScore(killReward);

        }

        if (other.gameObject.tag == "Missile")
        {
            Destroy(gameObject);
            addScore(killReward);
        }
    }
    void addScore(int value)
    {
        PlayerScore ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>();
        ps.updateScore(value);
    }
}

 
