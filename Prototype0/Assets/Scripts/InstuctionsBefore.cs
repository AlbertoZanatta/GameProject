﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstuctionsBefore : MonoBehaviour {

    private void Start()
    {
        Time.timeScale = 0f;
    }

    public void Ready()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
