using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler {

    public IInventoryItem Item { get; set; }
    public event EventHandler<InventoryEventArgs> itemReleased;

    private RectTransform rectTransform;
   
    // Use this for initialization
    void Start ()
    {
        rectTransform = GetComponent<RectTransform>();
	}

        public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        if(itemReleased != null && Item != null)
        {
            itemReleased(this, new InventoryEventArgs(Item));
        }
    }
}
