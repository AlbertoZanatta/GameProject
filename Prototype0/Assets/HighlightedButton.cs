using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightedButton : MonoBehaviour {

    private void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        if (button != null)
        {
            button.Select();
        }
    }
    
    private void OnEnable()
    {
        Button button = gameObject.GetComponent<Button>();
        if (button != null)
        {
            button.Select();
        }
    }
}

