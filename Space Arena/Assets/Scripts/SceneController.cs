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
    public AudioClip bgMusic;

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
        if (!inNewGameAnim || Input.GetKeyDown(KeyCode.Escape)) {
            Application.LoadLevel(2);
        }

        if (spaceshipThrustersOn)
        {
            if (spaceshipThrustersDelay >= 60)
            {
                spaceshipThruster1.Play();
                spaceshipThruster2.Play();
                spaceshipThruster1.gameObject.GetComponent<AudioSource>().Play();
                spaceshipThrustersDelay = Mathf.RoundToInt(Random.Range(0, 48));
            }
            else {
                spaceshipThrustersDelay++;
            }
        }
        if (astroThrusterOn) {
            if (astroThrusterDelay >= 48)
            {
                astroThruster.Play();
                astroThruster.gameObject.GetComponent<AudioSource>().Play();
                astroThrusterDelay = Mathf.RoundToInt(Random.Range(0, 24));
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
        iTween.MoveTo(cam, iTween.Hash(
                    "position", new Vector3(5.28f, 0.59f, 16.43f),
                    "easeType", "easeOutQuad",
                    "time", 37f));
        iTween.RotateTo(cam, iTween.Hash(
                    "rotation", new Vector3(10, 320, 0),
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
        logoBG.CrossFadeAlpha( 0.4f, 7f, false );
        logo.CrossFadeAlpha(1f, 7f, false);
        yield return new WaitForSeconds(7);
        logoBG.CrossFadeAlpha(0f, 3f, false);
        logo.CrossFadeAlpha(0f, 3f, false);
        yield return new WaitForSeconds(3);

        // spaceship arrives
        spaceshipThrustersOn = true;
        iTween.MoveTo(spaceship, iTween.Hash(
                    "position", new Vector3(-10f, -0.96f, 22.28f),
                    "easeType", "easeOutQuad",
                    "time", 10f));

        // change background music
        StartCoroutine(ChangeBgAudio());

        yield return new WaitForSeconds(3);
        // audio - "Going out for EVA"
        PlayNextClip();
        yield return new WaitForSeconds(7);
        iTween.MoveTo(spaceship, iTween.Hash(
                    "position", new Vector3(-23.72f, -0.96f, 22.28f),
                    "easeType", "easeOutQuad",
                    "time", 20f));
        spaceshipThrustersOn = false;

        astro.SetActive(true);
        // astronaut leaves spaceship
        astroThrusterOn = true;
        iTween.RotateTo(cam, iTween.Hash(
                    "rotation", new Vector3(0, 330, 0),
                    "easeType", "easeOutQuad",
                    "time", 10f));
        iTween.MoveTo(astro, iTween.Hash(
                    "position", new Vector3(-10, 1f, 26),
                    "orienttopath", true,
                    "easeType", "easeOutQuad",
                    "time", 4f));
        yield return new WaitForSeconds(4);
        astroThrusterOn = false;

        // audio - "Spotted some debris, better be careful"
        PlayNextClip();
        iTween.RotateTo(astro, iTween.Hash(
                    "rotation", new Vector3(0, 180, 0),
                    "easeType", "easeOutQuad",
                    "time", 8f));
        yield return new WaitForSeconds(8);

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
        yield return new WaitForSeconds(2);
        enemySpawner.SetActive(true);
        yield return new WaitForSeconds(2);
        // audio - "We are under attack!"
        PlayNextClip();
        enemySpawner.SetActive(false);
        yield return new WaitForSeconds(5);
        // launch missiles
        spaceship.GetComponent<missileLauncher>().startLaunch = true;
        yield return new WaitForSeconds(3);
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

    IEnumerator ChangeBgAudio()
    {
        AudioSource bgAudio = GameObject.FindGameObjectWithTag("Background").GetComponent<AudioSource>();
        float t = 0.15f;
        while (t > 0.0f)
        {
            t -= Time.deltaTime / 20;
            bgAudio.volume = t;
            yield return new WaitForSeconds(0.01f);
        }
        t = 0.0f;
        bgAudio.clip = bgMusic;
        bgAudio.volume = t;
        bgAudio.Play();
        while (t < 0.25f)
        {
            t += Time.deltaTime / 20;
            bgAudio.volume = t;
            yield return new WaitForSeconds(0.01f);
        }
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
