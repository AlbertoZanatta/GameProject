using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseClassScreen : MonoBehaviour {

   

    [SerializeField] private string screenId;
    public string ScreenId { get { return screenId; } }
    public static ScreenManager manager;

    public Button firstButton;

    protected EventSystem eventSystem
    {
        get { return GameObject.Find("EventSystem").GetComponent<EventSystem>(); }
    }

    public virtual void ShowWindow(bool show)
    {
        gameObject.SetActive(show);
    }

    public virtual void OpenWindow()
    {
        ShowWindow(true);
        if(firstButton != null)
        {
            firstButton.Select();
        } 
    }

    public virtual void CloseWindow()
    {
        ShowWindow(false);
    }

    
    protected virtual void Awake () {
        CloseWindow();
	}

}
