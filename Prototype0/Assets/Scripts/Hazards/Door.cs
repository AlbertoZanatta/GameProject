using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public float playerDistance;

    public DoorColumn[] doorColumns;
    private string item = "Level_Key";

    public void Open()
    {
        foreach(DoorColumn column in doorColumns)
        {
            column.Open();
        }
    }

    private void Update()
    {
        float distance = Mathf.Abs(Vector2.Distance(transform.position, PlayerController.Instance.transform.position));
        playerDistance = distance;
        SoundManager.instance.Drums(distance);
    }


    internal void Close()
    {
        foreach (DoorColumn column in doorColumns)
        {
            column.Close();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.inventory.HasItem(item))
            {
                Open();
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            Close();
        }
    }

    private void OnDisable()
    {
        SoundManager.instance.Drums(float.PositiveInfinity);
    }
}
