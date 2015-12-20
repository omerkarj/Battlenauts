using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public float ShotInterval = 3f;
    public GameObject EnemyShot;

    private GameObject player;
    private float shotInterval;
    private bool startShooting = false;


    // Use this for initialization
    void Start () {
        StartCoroutine(couroutineThatWaits());
        shotInterval = ShotInterval;
	}
    IEnumerator couroutineThatWaits()
    {
        yield return new WaitForSeconds(2.5f);
        startShooting = true;
    }
    // Update is called once per frame
    void Update () {

        if (startShooting)
        {
            shotInterval -= Time.deltaTime;
            if (shotInterval < 0)
            {
                Shoot();
                shotInterval = ShotInterval;
            }
        }
        

	}

    void Shoot()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        float angle = (Mathf.Atan2(
            player.transform.position.y - transform.position.y,
            player.transform.position.x - transform.position.x) - Mathf.PI / 2) * Mathf.Rad2Deg;

        GameObject shot = Instantiate(EnemyShot, transform.position, Quaternion.Euler(new Vector3(0f, 0f, angle))) as GameObject;
        Destroy(shot, 10f);
    }
}
