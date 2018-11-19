using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {
    public Door door;
    private string item = "Key_First_Level";
    public bool canOpen = false;

    private void Update()
    {
        if(canOpen)
        {
            door.Open();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if(player != null)
        {
            canOpen = player.inventory.HasItem(item);
        }
    }
}
