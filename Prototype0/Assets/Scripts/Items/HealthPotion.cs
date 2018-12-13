using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

[CreateAssetMenu()]
public class HealthPotion : IInventoryItem
{
    public int regain = 3;
    public override void OnDrop()
    {
        
    }

    public override void OnPickUp()
    {
        gameObject.SetActive(false);
    }

    public override void OnUse(Character character)
    {
        SoundManager.instance.DrinkPotion();
        character.Health.ChangeHealth(regain);
    }
}
