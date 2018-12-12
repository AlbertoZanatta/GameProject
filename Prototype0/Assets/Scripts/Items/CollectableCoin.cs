using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : MonoBehaviour {

    [SerializeField] float height = 1;
    [SerializeField] float speed = 0.5f;
    private Vector3 desiredPosition;
    private Vector3 initialPosition;
    private bool up = true;
   


    private void Start()
    {
        desiredPosition = transform.position + Vector3.up * height;
        initialPosition = transform.position;
    }

    private void Update()
    {
        if(up)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if(transform.position.y >= desiredPosition.y)
            {
                up = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y <= initialPosition.y)
            {
                up = true;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.instance.EarnCoin();
            Destroy(gameObject);
        }
    }
}

public class CollectedCoinArgs : EventArgs
{
  
}
