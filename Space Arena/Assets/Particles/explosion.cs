using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

    // Use this for initialization
    public ParticleSystem ps;
	void Start () {
        ps.Emit(100);
        StartCoroutine(animate());
	}

    IEnumerator animate()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
	
	}
}
