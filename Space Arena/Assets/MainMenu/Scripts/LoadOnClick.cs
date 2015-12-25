using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

public void LoadScene(int level)
    {
        Debug.Log("HERE");
        Application.LoadLevel(level);
    }
}

