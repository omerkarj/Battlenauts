using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScore : MonoBehaviour {

    // Use this for initialization
    public int Score;
	private int powerUpCounter;
    public Text scoreText;
	public int pointsForPowerUp=1000;
	void Start () {
        Score = 0;
        updateScore(0);
		powerUpCounter = 0;
 
	}
	
	// Update is called once per frame
	void Update () {
	}
   public void updateScore(int addValue)
    {
        Score += addValue;
        scoreText.text = "Score: " + Score;
		AddpowerUp (addValue);
    }
	private void AddpowerUp(int addValue){
		powerUpCounter += addValue;
		if (powerUpCounter >= pointsForPowerUp) {
			powerUpCounter=0;
			Debug.Log("power up is ready");
			gameObject.GetComponent<PlayerController>().isPowerupOn=true;
		}
	}
}
