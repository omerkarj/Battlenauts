using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScore : MonoBehaviour {

    // Use this for initialization
    public int Score;
    public Text scoreText;
	void Start () {
        Score = 0;
        updateScore(0);
 
	}
	
	// Update is called once per frame
	void Update () {
	}
   public void updateScore(int addValue)
    {
        Score += addValue;
        scoreText.text = "Score: " + Score;

    }
}
