using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBorder : MonoBehaviour {

    [SerializeField] bool entering;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.inCave = entering;
        }
    }
}
