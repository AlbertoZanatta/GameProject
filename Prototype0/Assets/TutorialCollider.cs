using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollider : MonoBehaviour {
    public GameObject screen;
    public bool mode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            screen.SetActive(mode);
            this.gameObject.SetActive(false);
        }
    }

    
}
