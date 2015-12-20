using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    private Animator _animator;
    private CanvasGroup _canvasGroup;
    public RectTransform rect;
    
    public bool IsOpen
    {
        get { return _animator.GetBool("IsOpen"); }
        set { _animator.SetBool("IsOpen", value); }
    }

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasGroup = GetComponent<CanvasGroup>();

        rect = GetComponent<RectTransform>();
        //rect.position = new Vector3(0, 0, 0);
        _canvasGroup.interactable = true;
        

    }

    public void Update()
    {
        //rect.position = new Vector3(0, 0, 0);
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("MainMenuOpen"))
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
        }
        else
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
        }

    }

}
