using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

public void LoadScene(int level)
    {
        PlayerPrefs.SetInt("LastLevel", Application.loadedLevel);
        PlayerPrefs.Save();

        GameObject bg = GameObject.FindGameObjectWithTag("Background");
        if (level == 0 && bg != null)
            Destroy(bg);
        Application.LoadLevel(level);
    }
}

