using UnityEngine;
using System.Collections;

public class homingMissile : MonoBehaviour {

    // Use this for initialization
    public float missileVelocity = 1;
    float turn = 20;
    Rigidbody missile;
    float delay=20;
    GameObject missileMod;
    public ParticleSystem smoke;
    private Transform target;
    public Transform explosion;
    private bool isInializing = false;
	void Start () {
        missile = gameObject.GetComponent<Rigidbody>();
        missileMod = gameObject;
		missile.velocity = new Vector3(0, missileVelocity*0.1F*Random.RandomRange(0.8F,1.8F) , 0);
        StartCoroutine(animate());
        Fire();
        missile.angularVelocity = new Vector3(-0.2F, 0, 0);
        
    }

    IEnumerator animate()
    {
        yield return new WaitForSeconds(Random.RandomRange(2F,6F));
        isInializing = true;
    }

	
	// Update is called once per frame
	void FixedUpdate () {
        if (target == null||missile==null||!isInializing)
        {
			if(isInializing)
			{
				destroyMissile();
			}
            return;

        }
        missile.velocity = transform.forward * missileVelocity;
       Quaternion targetRotation= Quaternion.LookRotation(target.position - transform.position);
        missile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));

	}
   void Fire()
    {
        float distance=Mathf.Infinity;
        GameObject go = GameObject.FindGameObjectWithTag("target");
		if (go != null) {
			go.tag = "targeted";
			float diff = (go.transform.position - transform.position).sqrMagnitude;
			if (diff < distance) {
				distance = diff;
				target = go.transform;
			}
		}
    }
    void OnTriggerEnter(Collider theCollision)
    {
		if (theCollision.gameObject.tag == "targeted") {
			destroyMissile();
            Destroy(target.gameObject);
		}

		else {
			if (theCollision.gameObject.tag == "target"){
				target.tag="target";
                Destroy(target.gameObject);
            }
			Physics.IgnoreCollision(missile.GetComponent<Collider>(),theCollision.gameObject.GetComponent<Collider>());
		}
    }
	void destroyMissile(){
		smoke.emissionRate = 0.0f;
		Destroy (gameObject);
		Instantiate (explosion, transform.position, Quaternion.identity);
	}
}
