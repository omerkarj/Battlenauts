using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

    public GameObject astro;
    public GameObject spaceship;
    public ParticleSystem spaceshipThruster1;
    public ParticleSystem spaceshipThruster2;
    public ParticleSystem astroThruster;

    private bool inNewGameAnim;
    private bool spaceshipThrustersOn;
    private int spaceshipThrustersDelay = 0;
    private bool astroThrusterOn;
    private int astroThrusterDelay = 0;

	// Use this for initialization
	void Start () {
        inNewGameAnim = true;
        StartCoroutine(NewGameAnim());
	}

    void Update()
    {
        if (!inNewGameAnim) {
            Application.LoadLevel(2);
        }

        if (spaceshipThrustersOn)
        {
            if (spaceshipThrustersDelay >= 24)
            {
                spaceshipThruster1.Play();
                spaceshipThruster2.Play();
                spaceshipThrustersDelay = 0;
            }
            else {
                spaceshipThrustersDelay++;
            }
        }
        if (astroThrusterOn) {
            if (astroThrusterDelay >= 24)
            {
                astroThruster.Play();
                astroThrusterDelay = 0;
            }
            else {
                astroThrusterDelay++;
            }
        }    
    }

    IEnumerator NewGameAnim()
    {
        GameObject cam = gameObject;

        // spaceship arrives
        spaceshipThrustersOn = true;
        iTween.MoveFrom(spaceship, iTween.Hash(
                    "position", new Vector3(-100, -2, -10f),
                    "easeType", "easeOutQuad",
                    "time", 10f));

        yield return new WaitForSeconds(10);
        spaceshipThrustersOn = false;

        astro.SetActive(true);
        // astronaut leaves spaceship
        astroThrusterOn = true;
        iTween.MoveTo(astro, iTween.Hash(
                    "position", new Vector3(0, 1f, 26),
                    "orienttopath", true,
                    "easeType", "easeOutQuad",
                    "time", 8f));
        iTween.RotateTo(cam, new Vector3(0, 330, 0), 8f);

        yield return new WaitForSeconds(8);
        astroThrusterOn = false;

        // astronaut head up to Z=0
        astroThrusterOn = true;
        iTween.MoveTo(astro, iTween.Hash(
                    "position", new Vector3(0.07f, -0.601f, -0.2f),
                    "orienttopath", true,
                    "easeType", "easeOutQuad",
                    "time", 10f));
        iTween.RotateTo(astro, new Vector3(0, 110, 0), 10f);
        iTween.MoveTo(cam, iTween.Hash(
                    "position", new Vector3(0, 0, -10f),
                    "looktarget", astro.transform,
                    "easeType", "easeOutQuad",
                    "time", 10f));

        yield return new WaitForSeconds(10);
        astroThrusterOn = false;

        // camera rotates to face the game scene and initiate game
        iTween.RotateTo(cam, new Vector3(0, 0, 0), 3f);
        yield return new WaitForSeconds(3);
        inNewGameAnim = false;
    }
}
