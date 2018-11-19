using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = transform;
        Debug.Log("On platform: " + collision.gameObject.tag);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = null;
    }
}
