using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneController : MonoBehaviour {

    public GameObject astro;
    public GameObject spaceship;
    public GameObject enemySpawner;
    public Text logo;
    public Image logoBG;
    public ParticleSystem spaceshipThruster1;
    public ParticleSystem spaceshipThruster2;
    public ParticleSystem astroThruster;
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private int currentAudioClip;
    private bool inNewGameAnim;
    private bool spaceshipThrustersOn;
    private int spaceshipThrustersDelay = 0;
    private bool astroThrusterOn;
    private int astroThrusterDelay = 0;

	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        currentAudioClip = 0;
        logo.canvasRenderer.SetAlpha(0.0f);
        logoBG.canvasRenderer.SetAlpha(0.0f);

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

        // Malerion 7 camera movement
        iTween.MoveFrom(cam, iTween.Hash(
                    "position", new Vector3(0, 0, -10f),
                    "easeType", "easeOutQuad",
                    "time", 37f));
        iTween.RotateFrom(cam, iTween.Hash(
                    "rotation", new Vector3(0, 0, 0),
                    "easeType", "easeOutQuad",
                    "time", 27f));
        // audio - "Malerion 7 narration"
        PlayNextClip();
        yield return new WaitForSeconds(27);
        iTween.RotateTo(cam, iTween.Hash(
                    "rotation", new Vector3(10, 300, 0),
                    "easeType", "easeOutQuad",
                    "time", 10f));
        // fade logo in and out
        logoBG.CrossFadeAlpha( 0.2f, 7f, false );
        logo.CrossFadeAlpha(1f, 7f, false);
        yield return new WaitForSeconds(7);
        logoBG.CrossFadeAlpha(0f, 3f, false);
        logo.CrossFadeAlpha(0f, 3f, false);
        yield return new WaitForSeconds(3);

        // spaceship arrives
        spaceshipThrustersOn = true;
        iTween.MoveTo(spaceship, iTween.Hash(
                    "position", new Vector3(0, 0, 21.1f),
                    "easeType", "easeOutQuad",
                    "time", 10f));
        yield return new WaitForSeconds(3);
        // audio - "Going out for EVA"
        PlayNextClip();
        yield return new WaitForSeconds(7);
        spaceshipThrustersOn = false;

        astro.SetActive(true);
        // astronaut leaves spaceship
        astroThrusterOn = true;
        iTween.RotateTo(cam, iTween.Hash(
                    "rotation", new Vector3(0, 330, 0),
                    "easeType", "easeOutQuad",
                    "time", 10f));
        iTween.MoveTo(astro, iTween.Hash(
                    "position", new Vector3(0, 1f, 26),
                    "orienttopath", true,
                    "easeType", "easeOutQuad",
                    "time", 4f));
        yield return new WaitForSeconds(4);
        // audio - "Spotted some debris, better be careful"
        PlayNextClip();
        iTween.RotateTo(astro, iTween.Hash(
                    "rotation", new Vector3(0, 180, 0),
                    "easeType", "easeOutQuad",
                    "time", 6f));
        yield return new WaitForSeconds(6);
        astroThrusterOn = false;

        // astronaut head up to Z=0
        astroThrusterOn = true;
        iTween.MoveTo(astro, iTween.Hash(
                    "position", new Vector3(0.07f, -0.601f, -0.2f),
                    "orienttopath", true,
                    "easeType", "easeOutQuad",
                    "time", 13f));
        iTween.MoveTo(cam, iTween.Hash(
                    "position", new Vector3(0, 0, -10f),
                    "looktarget", astro.transform,
                    "easeType", "easeOutQuad",
                    "time", 13f));

        enemySpawner.SetActive(true);
        yield return new WaitForSeconds(2);
        // audio - "We are under attack!"
        PlayNextClip();
        enemySpawner.SetActive(false);
        yield return new WaitForSeconds(5);
        // launch missiles
        spaceship.GetComponent<missileLauncher>().startLaunch = true;
        yield return new WaitForSeconds(5);
        astroThrusterOn = false;

        // camera rotates to face the game scene and initiate game
        iTween.RotateTo(astro, iTween.Hash(
                    "rotation", new Vector3(0, 110, 0),
                    "easeType", "easeOutQuad",
                    "time", 3f));
        iTween.RotateTo(cam, iTween.Hash(
                    "rotation", new Vector3(0, 0, 0),
                    "easeType", "easeOutQuad",
                    "time", 3f));
        yield return new WaitForSeconds(4);
        // audio - "Hold them off until I can get more missiles ready"
        PlayNextClip();
        yield return new WaitForSeconds(4);
        inNewGameAnim = false;
    }

    void PlayNextClip()
    {
        if (currentAudioClip < audioClips.Length)
        {
            audioSource.clip = audioClips[currentAudioClip];
            audioSource.Play();
            currentAudioClip++;
        }
    }
}
