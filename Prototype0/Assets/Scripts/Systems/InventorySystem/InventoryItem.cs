using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem : MonoBehaviour {
    [SerializeField] int _quantity;
    [SerializeField] ScriptableInventoryItem _item;

    public int quantity
    {
        get { return _quantity; }
    }

    public string itemID
    {
        get { return _item.ID; }
    }

    public void SetItem(ScriptableInventoryItem item)
    {
        if(item != null)
        {
            _item = item;
            _quantity++;
        }
        
    }

    public void AddOther()
    {
        if(_item != null)
        {
            _quantity++;
        }
    }

    public void OnUse(PlayerController player) { }
}
