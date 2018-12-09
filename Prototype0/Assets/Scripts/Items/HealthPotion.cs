using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

[CreateAssetMenu()]
public class HealthPotion : IInventoryItem
{
    public int regain = 1;
    public override void OnDrop()
    {
        
    }

    public override void OnPickUp()
    {
        //collectText.ShowText();
        Destroy(gameObject);
    }

    public override void OnUse(Character character)
    {
        character.Health.ChangeHealth(regain);
    }
}
