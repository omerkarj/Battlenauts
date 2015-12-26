using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreList : MonoBehaviour {

    public GameObject entryPrefab;

	// Use this for initialization
	void Start () {
        for (int i = 1; i <= 10; i++)
        {
            GameObject entry = Instantiate(entryPrefab) as GameObject;
            HighScoreValues highScoreValues = entry.GetComponent<HighScoreValues>();

            highScoreValues.SetNumber(i + ".");
            string name = PlayerPrefs.HasKey("HighScore" + i + "Name") ? PlayerPrefs.GetString("HighScore" + i + "Name") : "Battlenaut" + i;
            highScoreValues.SetName(name);
            string score = PlayerPrefs.HasKey("HighScore" + i + "Score") ? PlayerPrefs.GetString("HighScore" + i + "Score").ToString() : "0";
            highScoreValues.SetScore(PlayerPrefs.GetInt("HighScore" + i + "Score").ToString());

            entry.transform.SetParent(transform, false);
            entry.GetComponent<RectTransform>().anchoredPosition = new Vector3(20, 110 - 20 * i, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
