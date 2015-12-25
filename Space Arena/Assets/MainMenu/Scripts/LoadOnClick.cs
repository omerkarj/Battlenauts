using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

public void LoadScene(int level)
    {
        PlayerPrefs.SetInt("lastLevel", Application.loadedLevel);
        Application.LoadLevel(level);
    }
}

