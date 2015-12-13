using UnityEngine;
using System.Collections;

public class missileLauncher : MonoBehaviour {
	public bool startLaunch=false;
	public int launchNumber = 1;
	public Transform missile;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
        anim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	if (startLaunch) {
            startLaunch = false;
			StartCoroutine(fireAndAnimate());

		}
	}

	IEnumerator fireAndAnimate(){
        bool reset = true;
        anim.enabled = true;
        anim.Play ("door open"); //play animation
        yield return new WaitForSeconds(2f);
        if (reset) {
			for(int i=0;i<launchNumber;i++){
				Vector3 startPosition= new Vector3(transform.position.x-1+(i*0.8F),transform.position.y-1F,transform.position.z);
				Instantiate(missile,startPosition,missile.rotation);
			}
			yield return new WaitForSeconds(1F);

			anim.Play ("idle"); //play animation
			yield return new WaitForSeconds(0.5F);
			anim.Stop ();
            reset = false;
            anim.enabled = false;
        }
	}
}
