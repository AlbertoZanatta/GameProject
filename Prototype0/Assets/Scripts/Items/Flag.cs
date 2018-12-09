using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : IInventoryItem {

    public override void OnDrop()
    {
       
    }

    public override void OnPickUp()
    {
        Destroy(gameObject);
    }

    public override void OnUse(Character character)
    {
        character.Health.Refill();
    }
}
