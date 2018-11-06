using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemSlotController : MonoBehaviour {

    public Text text;
    public Image imageFront;

	
	public void AddItem(Sprite img, string txt)
    {
        text.text = txt;
        imageFront.sprite = img;
        imageFront.enabled = true;
    }

    public void RemoveItem()
    {
        text.text = "";
        imageFront.enabled = false;
    }
	
}
