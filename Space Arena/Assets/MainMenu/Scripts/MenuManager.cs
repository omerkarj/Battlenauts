using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public Menu currentMenu;
    private AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;


    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.4f;

        StartCoroutine(ChangeMenu(currentMenu));
    }


    public void ShowMenu(Menu menu)
    {
        StartCoroutine(ChangeMenu(menu));
    }
    IEnumerator ChangeMenu(Menu menu)
    {
        if (currentMenu != null && currentMenu.IsOpen)
        {
            currentMenu.IsOpen = false;
            yield return new WaitForSeconds(0.6f);
        }
        currentMenu = menu;
        currentMenu.IsOpen = true;
    }

    public void PlayHover()
    {
        audioSource.clip = hoverSound;
        audioSource.Play();
    }
    public void PlayClick()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}

