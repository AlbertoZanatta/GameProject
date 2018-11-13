using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler {

    private RectTransform rectTransform;

    public void OnDrop(PointerEventData eventData)
    {
        //method to check if the current mouse coordinates are inside the rectangle of the inventory panel
        if(!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
        {
            Debug.Log("Drop item!");
        }
    }

    // Use this for initialization
    void Start ()
    {
        rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
