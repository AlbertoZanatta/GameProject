using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : SpecialLock {

    public float playerDistance;

    public DoorColumn[] doorColumns;

    public void OpenColumns()
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

    protected override void Open()
    {
        base.Open();
        OpenColumns();
    }

    public void CloseColumns()
    {
        foreach (DoorColumn column in doorColumns)
        {
            column.Close();
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if(open)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                CloseColumns();
            }
        }   
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (open)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OpenColumns();
            }
        }
    }

    private void OnDisable()
    {
        SoundManager.instance.Drums(float.PositiveInfinity);
    }

    public void Close()
    {
        CloseColumns();
        open = false;
    }
}
