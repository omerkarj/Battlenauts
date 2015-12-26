using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    private Animator _animator;
    private CanvasGroup _canvasGroup;
    private AudioSource _audioSource;
    public AudioClip slideInSound;
    public AudioClip slideOutSound;
    
    public bool IsOpen
    {
        get { return _animator.GetBool("IsOpen"); }
        set { 
            _animator.SetBool("IsOpen", value);
            StartCoroutine(SlideWithDelay(value));
        }
    }

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.volume = 0.4f;

        _canvasGroup.interactable = true;
    }

    public void Update()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("MainMenuOpen"))
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
        }
        else
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
        }

    }

    IEnumerator SlideWithDelay(bool val)
    {
        float delay;
        if (val)
        {
            _audioSource.clip = slideInSound;
            delay = 0f;
        }
        else
        {
            _audioSource.clip = slideOutSound;
            delay = 1.7f;
        }
        yield return new WaitForSeconds(delay);
        _audioSource.Play();
    }

}
