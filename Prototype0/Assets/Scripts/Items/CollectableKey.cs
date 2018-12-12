using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : IInventoryItem {
   
    public override void OnDrop()
    {
        
    }

    public override void OnPickUp()
    {
        gameObject.SetActive(false);
    }

    public override void OnUse(Character character)
    {
        
    }
   
}
