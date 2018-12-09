using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteTrigger : MonoBehaviour {
    public Stalactite[] stalactites;
    public float delay = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(DropStalactites());
            Debug.Log("Dropping stalactites");
        }
    }

    private IEnumerator DropStalactites()
    {
        foreach(Stalactite stalactite in stalactites)
        {
            stalactite.Drop();
            yield return new WaitForSeconds(delay);
        }

        this.gameObject.SetActive(false);
    }
}
