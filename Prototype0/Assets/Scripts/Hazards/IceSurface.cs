using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSurface : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if(player != null)
        {
            player.OnIce = true;

            if (Mathf.Abs(player.characterRigidbody.velocity.y) <= 0.01)
            {
                player.characterRigidbody.velocity = new Vector2(player.characterRigidbody.velocity.x * 0.5f, player.characterRigidbody.velocity.y);
            }
        }
        
       
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if(player != null)
        {
            player.OnIce = false;
        }   
    }
}
