using UnityEngine;
using System.Collections;

public class TargetLock : MonoBehaviour {
    private GameObject middle;
    private GameObject enemy;
    public Animator anim;
    private bool trig = true;

	// Use this for initialization
	void Start () {
        middle = GameObject.FindGameObjectWithTag("middle");
        enemy = GameObject.FindGameObjectWithTag("target");
    }
	
	// Update is called once per frame
	void Update () {
        //gameObject.transform.position = new Vector3(middle.transform.position.x,
                                                 //   middle.transform.position.y,
                                                //    middle.transform.position.z - .7f);
        gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        //if(enemy.tag == "targeted")
       // {
        //    anim.Play("isTriggered");
       // }
	}

    public void Destroy()
    {

        Destroy(gameObject);
    }
}
