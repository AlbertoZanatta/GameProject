using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour {

    [System.Serializable]
    public class LootObject
    {
        public GameObject dropPrefab;
        public float dropOdds;
    }

    public LootObject[] lootObjects;
    
    public void Drop(Vector3 dropPoint)
    {
        foreach(LootObject lootObject in lootObjects)
        {
            float odds = Random.value;
            if(odds <= lootObject.dropOdds)
            {
                GameObject collectable = Instantiate(lootObject.dropPrefab, dropPoint, Quaternion.identity);
                IInventoryItem item = collectable.GetComponent<IInventoryItem>();
                if(item != null)
                {
                    item.enemyDropped = true;
                }
            }
        }
    }
}


