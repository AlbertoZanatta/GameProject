using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

[CreateAssetMenu()]
public class HealthPotion : MonoBehaviour, IInventoryItem
{
    public Sprite sprite;
    public int regain = 1;
    //public CollectText collectText;

    void Start()
    {
    }

    public string itemName
    {
        get
        {
            return "HealthPotion";
        }
    }

    public Sprite itemImage
    {
        get
        {
            return sprite;
        }
    }

    public void OnDrop()
    {
        
    }

    public void OnPickUp()
    {
        //collectText.ShowText();
        Destroy(gameObject);
    }

    public void OnUse(Character character)
    {
        character.Health.ChangeHealth(regain);
    }
}
