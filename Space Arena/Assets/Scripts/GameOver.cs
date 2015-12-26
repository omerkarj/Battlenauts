using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

    public AudioClip gameOverAudio;
    public AudioClip newHighScoreAudio;
    public Text scoreText;

    private AudioSource audioSource;
    private Animator anim;
    private PlayerMovement pm;
    private bool triggered = false;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (pm.isDead && !triggered)
        {
            int playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>().score;
            scoreText.text = "Score " + playerScore;
            anim.SetTrigger("gameOver");
            audioSource.clip = gameOverAudio;
            audioSource.Play();

            StartCoroutine(CheckHighScore(playerScore));
        }
	}

    IEnumerator CheckHighScore(int playerScore)
    {
        if (!triggered)
        {
            triggered = true;
            yield return new WaitForSeconds(2);

            Debug.Log("check high score started");
            bool swap = false;

            int i = 1;
            // find the entry to replace with current score
            while (i <= 10)
            {
                int scoreInEntry = PlayerPrefs.GetInt("HighScore" + i + "Score");
                if (!PlayerPrefs.HasKey("HighScore" + i + "Score") || (playerScore > PlayerPrefs.GetInt("HighScore" + i + "Score")))
                {
                    swap = true;
                    break;
                }
                i++;
            }

            if (swap)
            {
                if (i == 1)
                {
                    audioSource.clip = newHighScoreAudio;
                    audioSource.Play();
                }

                // replace following entries
                int j = 10;
                while (j > i)
                {
                    int scoreInAboveEntry = PlayerPrefs.GetInt("HighScore" + (j - 1) + "Score");
                    string nameInAboveEntry = PlayerPrefs.GetString("HighScore" + (j - 1) + "Name");
                    PlayerPrefs.SetInt("HighScore" + j + "Score", scoreInAboveEntry);
                    PlayerPrefs.SetString("HighScore" + j + "Name", nameInAboveEntry);
                    j--;
                }
                // replace this entry
                PlayerPrefs.SetInt("HighScore" + i + "Score", playerScore);
                string playerName = "Battlenaut" + PlayerPrefs.GetInt("TimesPlayed");
                PlayerPrefs.SetString("HighScore" + i + "Name", playerName);
                PlayerPrefs.Save();
            }
        }
    }
}
