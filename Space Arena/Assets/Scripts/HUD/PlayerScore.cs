using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScore : MonoBehaviour {

    public int score;
    public int powerUpCounter;
    public Text scoreText;
    public int pointsForPowerUp = 1000;
    public AudioClip newHighScore;
    public AudioClip powerUpReady;
    
    private AudioSource audioSource;
    private int highScore;

    // Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();

        score = 0;
        updateScore(0);
		powerUpCounter = 0;
        highScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
	}
	
	// Update is called once per frame
	void Update () {
	}
   public void updateScore(int addValue)
    {
        score += addValue;
        scoreText.text = "Score: " + score;

        if (highScore > 0 && score > highScore)
        {
            audioSource.clip = newHighScore;
            audioSource.Play();
            //PlayerPrefs.SetInt("HighScore", score);
        }

		AddpowerUp (addValue);
    }
	private void AddpowerUp(int addValue){
		SpecialPower sp = gameObject.GetComponent<SpecialPower> ();

		if (powerUpCounter >= pointsForPowerUp) {
			sp.PowerUp (pointsForPowerUp, pointsForPowerUp);
            Debug.Log ("power up is ready");
			gameObject.GetComponent<PlayerController> ().isPowerupOn = true;
		} else {
			sp.PowerUp (powerUpCounter, pointsForPowerUp);
			powerUpCounter += addValue;
		}
	}
}
