using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

public void LoadScene(int level)
    {
        PlayerPrefs.SetInt("LastLevel", Application.loadedLevel);
        PlayerPrefs.Save();
        Application.LoadLevel(level);
    }
}

