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
	}
	
	// Update is called once per frame
	void Update () {
	if (startLaunch) {
			StartCoroutine(fireAndAnimate());

		}
	}

	IEnumerator fireAndAnimate(){
		anim.enabled = true;
		anim.Play ("door open"); //play animation
		yield return new WaitForSeconds(2.5F);
		if (startLaunch) {
			for(int i=0;i<launchNumber;i++){
				Vector3 startPosition= new Vector3(transform.position.x-1+(i*0.8F),transform.position.y-1F,transform.position.z);
				Instantiate(missile,startPosition,missile.rotation);
			}
			startLaunch=false;
			yield return new WaitForSeconds(1F);
			anim.Play ("door open"); //play animation
			yield return new WaitForSeconds(0.5F);
			anim.Stop ();
			anim.enabled=false;
		}

	}
}
