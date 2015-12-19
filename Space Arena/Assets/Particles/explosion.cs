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
        yield return new WaitForSeconds(0.8F);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
	
	}
}
