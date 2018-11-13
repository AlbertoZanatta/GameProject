using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Knife : MonoBehaviour, IInventoryItem
{
    public Sprite sprite;
    public GameObject knifePrefab;
    //public CollectText collectText;

    void Start()
    {
    }

    public string itemName
    {
        get
        {
            return "Knife";
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
        character.Throw(knifePrefab);
    }
}