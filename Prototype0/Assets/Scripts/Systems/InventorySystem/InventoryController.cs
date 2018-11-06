using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    public ItemSlotController[] itemImages = new ItemSlotController[numSlots];
    public ScriptableInventoryItem[] items = new ScriptableInventoryItem[numSlots];
    public int[] counts = new int[numSlots];

    public const int numSlots = 4;

    private int count = 0;

    public bool AddItem(ScriptableInventoryItem item)
    {
        if(item.stackable)
        {
            int i = 0;
            bool found = false;
            for(; i < count && !found; i++)
            {
                if (items[i].Equals(item))
                    found = true;
            }
            if (found)
            {
                counts[i-1]++;
                itemImages[i - 1].AddItem(item.image, counts[i - 1].ToString());
                return true;
            }
            else
            {
                if(i < numSlots)
                {
                    counts[i]++;
                    items[i] = item;
                    itemImages[i].AddItem(item.image, counts[i].ToString());
                    count++;
                    return true;
                }

                return false;
            }

        }
        else if(count < items.Length)
        {
            items[count] = item;
            Debug.Log("Setting inventory image at "+ count);
            itemImages[count].AddItem(item.image, "");
            count++;
            return true;
        }
        return false;
    }

    public bool RemoveItem(ScriptableInventoryItem item)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i] == item)
            {
                for(int j = i+1; j < items.Length; j++)
                {
                    items[j - 1] = items[j];
                    itemImages[j - 1].RemoveItem();
                    if (counts[j] > 0)
                        counts[j] = 0;
                }
                count--;
                return true;
            }
        }
        return false;
    }


}
