using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrownLine : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character drowned = collision.gameObject.GetComponent<Character>();
        if(drowned != null)
        {
            drowned.Die();
        }
        else
        {
            collision.gameObject.SetActive(false);
        }
    }
}
