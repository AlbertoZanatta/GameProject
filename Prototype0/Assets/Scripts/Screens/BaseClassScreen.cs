using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseClassScreen : MonoBehaviour {

    [SerializeField] private string screenId;
    public string ScreenId { get { return screenId; } }
    public GameObject OptionOne;
    public static ScreenManager manager;


    

    protected EventSystem eventSystem
    {
        get { return GameObject.Find("EventSystem").GetComponent<EventSystem>(); }
    }

    public virtual void ShowWindow(bool show)
    {
        gameObject.SetActive(show);
    }


    public void PassSelectedItem()
    {
        eventSystem.SetSelectedGameObject(OptionOne);
    }

    public virtual void OpenWindow()
    {
        ShowWindow(true);
        PassSelectedItem();
    }

    public virtual void CloseWindow()
    {
        ShowWindow(false);
    }

    
    protected virtual void Awake () {
        CloseWindow();
	}

}
