using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu()]
public class ScriptableInventoryItem : ScriptableObject
{

    public string itemName;
    public Sprite image;
    public bool stackable;

}
