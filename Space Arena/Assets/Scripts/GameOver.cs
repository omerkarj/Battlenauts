using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    Animator anim;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        PlayerHealth ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        if (ph.currentHealth <= 0)
            anim.SetTrigger("gameOver");

	}
}
