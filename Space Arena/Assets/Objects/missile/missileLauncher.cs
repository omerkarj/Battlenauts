﻿using UnityEngine;
using System.Collections;


public class missileLauncher : MonoBehaviour
{
    public bool startLaunch = false;
    public int launchNumber = 1;
    public Transform missile;
    Rigidbody r;
    Animator anim;
    Vector3 startPosition;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
            anim.enabled = false;
            r = gameObject.GetComponent<Rigidbody>();
            startPosition = transform.position;
        }
	
	// Update is called once per frame
    void Update()
    {
        if (startLaunch)
        {
            startLaunch = false;
            StartCoroutine(fireAndAnimate());

        }
    }

    IEnumerator fireAndAnimate()
    {
        //transform.position = startPosition;
        r.velocity = Vector3.zero;
        bool reset = true;
        anim.enabled = true;
        anim.Play("door open"); //play animation
        float zOffset=0F;
        float xOffset = 0F;
        yield return new WaitForSeconds(2f);
        if (reset)
        {
            for (int i = 0; i < launchNumber; i++)
            {
                Vector3 startPosition = new Vector3(transform.position.x - 1 + (xOffset * 0.6F), transform.position.y -1F, transform.position.z+zOffset);
                Instantiate(missile, startPosition, missile.rotation);
                xOffset++;
                if ((i+1)%6 ==0)
                {
                    zOffset++;
                    xOffset = 0;
                }
            }
            yield return new WaitForSeconds(1F);

            anim.Play("idle"); //play animation
            yield return new WaitForSeconds(0.5F);
            anim.Stop();
            reset = false;
            anim.enabled = false;
        }
    }
    
}
