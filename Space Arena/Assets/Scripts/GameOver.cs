using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    Animator anim;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        PlayerHealth ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        if (ph.currentHealth <= 0)
        {
            anim.SetTrigger("gameOver");
            CheckHighScore();
        }

	}

    void CheckHighScore()
    {
        int playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>().score;

        int i = 1;
        // find the entry to replace with current score
        while (i <= 10)
        {
            int scoreInEntry = PlayerPrefs.GetInt("HightScore" + i + "Score");
            if (!PlayerPrefs.HasKey("HighScore" + i + "Score") || playerScore > PlayerPrefs.GetInt("HightScore" + i + "Score"))
                break;
            i++;
        }

        // replace this entry and all following entries
        int j = 10;
        while (j > i)
        {
            int scoreInEntry = PlayerPrefs.GetInt("HighScore" + j + "Score");
            PlayerPrefs.SetInt("HighScore" + (j - 1) + "Score", scoreInEntry);
            // TODO: set name too
            j--;
        }
        PlayerPrefs.SetInt("HighScore" + i + "Score", playerScore);

        PlayerPrefs.Save();
    }
}
