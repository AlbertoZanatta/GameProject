using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLavaPlatform : MonoBehaviour {

    bool move = false;
    [SerializeField] float speed = 3f;
    Vector3 desiredPosition;
    private void Start()
    {
        desiredPosition = transform.position - new Vector3(0, 2, 0);
    }
    private void Update()
    {
        if(move)
        {
            if(transform.position == desiredPosition)
            {
                gameObject.SetActive(false);
            }
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            move = true;
        }
    }
}
