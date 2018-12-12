using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Knife : IInventoryItem
{
    public GameObject knifePrefab;

    

    public override void OnDrop()
    {

    }

    public override void OnPickUp()
    {
        gameObject.SetActive(false);
    }

    public override void OnUse(Character character)
    {
        if(character.spawnPoint != null)
        {
            character.Throw(knifePrefab, character.spawnPoint.position);
        }
        else
        {
            character.Throw(knifePrefab, character.transform.position);
        }
        
    }
}