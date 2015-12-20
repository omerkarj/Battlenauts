using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

    // Use this for initialization
    public float destroyDelay=0.8F;
	void Start () {
        StartCoroutine(animate());
	}

    IEnumerator animate()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
	
	}
}
