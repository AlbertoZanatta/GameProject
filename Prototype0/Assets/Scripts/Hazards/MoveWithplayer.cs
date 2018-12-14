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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player collided with waiting platform");
        if(collision.gameObject.tag == "Player")
        {
            moveScript.enabled = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveScript.enabled = false;
        }
    }
}
