using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreValues : MonoBehaviour {

    public GameObject number, name, score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetNumber(string value)
    {
        number.GetComponent<Text>().text = value;
    }
    
    public void SetName(string value)
    {
        name.GetComponent<Text>().text = value;
    }
    
    public void SetScore(string value)
    {
        score.GetComponent<Text>().text = value;
    }
}
