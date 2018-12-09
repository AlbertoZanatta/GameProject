using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColumn : MonoBehaviour {

    [SerializeField] float vdispose = 5f;
    [SerializeField] float hdispose = 0f;
    [SerializeField] float speed = 1f;

    private Vector3 closePosition;
    private Vector3 openPosition;
    private bool open;
    private bool close;


	// Use this for initialization
	void Start ()
    {
        closePosition = transform.position;
        openPosition = new Vector3(closePosition.x + hdispose, closePosition.y + vdispose, transform.position.z);
	}

    private void Update()
    {
        if(open)
        {
           
            if(transform.position == openPosition)
            {
                open = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, openPosition, speed * Time.deltaTime);
            }
            
        }
        else if(close)
        {    
            if (transform.position == closePosition)
            {
                close = false;
                  
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, closePosition, speed * Time.deltaTime);
            } 
        }
    }

    public void Open()
    {
        open = true;
        close = false;
    }

    public void Close()
    {
        close = true;
        open = false;
    }


}
