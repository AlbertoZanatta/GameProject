using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanoTrap : MonoBehaviour {
    bool moveDown = false;
    public Transform desiredPosition;
    float speed = 4f;

    private void Update()
    {
        if(moveDown)
        {
            if(transform.position == desiredPosition.position)
            {
                moveDown = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, desiredPosition.position, speed * Time.deltaTime);
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveDown = true;
            collision.gameObject.transform.parent = transform;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
            DontDestroyOnLoad(collision.gameObject);
        }

    }
}
