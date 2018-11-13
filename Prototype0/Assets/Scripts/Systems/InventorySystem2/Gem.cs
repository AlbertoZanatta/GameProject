using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IInventoryItem {
    [SerializeField] private Sprite _image;

    public string itemName
    {
        get
        {
            return "Gem";
        }
    }

    public Sprite itemImage
    {
        get { return _image; }
    }

    public void OnDrop()
    {
        throw new System.NotImplementedException();
    }

    public void OnUse(Character character)
    {
        
    }

    void IInventoryItem.OnPickUp()
    {
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
