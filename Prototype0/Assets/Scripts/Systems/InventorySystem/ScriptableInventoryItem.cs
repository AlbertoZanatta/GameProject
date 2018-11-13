using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu()]
public abstract class ScriptableInventoryItem : ScriptableObject
{

    public string itemName;
    public Sprite image;
    public bool stackable;
    public string ID; //each inventory item has a unique id;


}
