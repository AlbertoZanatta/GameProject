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

    public void Close()
    {
        CloseColumns();
        open = false;
    }
}
