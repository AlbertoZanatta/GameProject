using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPillar : MonoBehaviour {


    [SerializeField] float height = 1;
    [SerializeField] float speed = 0.5f;
    private Vector3 desiredPosition;
    private Vector3 initialPosition;
    private Vector3 otherPosition;



    private void Start()
    {
        otherPosition = transform.position + Vector3.up * height;
        initialPosition = transform.position;
        desiredPosition = otherPosition;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, step);
        if(transform.position == desiredPosition)
        {
            desiredPosition = desiredPosition == otherPosition ? initialPosition : otherPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = null;
    }
}
