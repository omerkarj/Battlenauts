using UnityEngine;
using System.Collections;





public class PauseMenu : MonoBehaviour {


    private Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().pauseToggle)
        {
            Debug.Log("HERHEHREHR");
            anim.SetTrigger("pause");
        }
	}
}
