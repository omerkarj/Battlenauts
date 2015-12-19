using UnityEngine;
using System.Collections;
using System;

public class Enemy2SmallMovement : MonoBehaviour {

    
    public float distance = 1f;
    public float speed = 2f;



	// Use this for initialization
	void Start () {
        iTween.PunchPosition(gameObject, new Vector3(3, 0, 0), 5f);
        StartCoroutine(couroutineThatWaits());
        
	}

    private IEnumerator couroutineThatWaits()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(5f);
        iTween.MoveTo(gameObject, player.transform.position, 2f);
    }

    // Update is called once per frame
    void Update () {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}

 
