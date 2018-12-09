using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithplayer : MonoBehaviour {
    MovingPlatform moveScript;

	// Use this for initialization
	void Start () {
        moveScript = GetComponent<MovingPlatform>();
        moveScript.enabled = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player on platform!");
            moveScript.enabled = true;
        }
    }
}
