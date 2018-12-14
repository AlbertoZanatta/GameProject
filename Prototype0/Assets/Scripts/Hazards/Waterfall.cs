﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Waterfall : MonoBehaviour {
    private static int numWaterfalls;
    [SerializeField] private int numWaterfall;
    private void Start()
    {
        numWaterfalls++;
        numWaterfall = numWaterfalls;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene arg0, LoadSceneMode arg1)
    {
        numWaterfalls = 0;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    // Update is called once per frame
    void Update ()
    {
        float distance = Mathf.Abs(transform.position.x - PlayerController.Instance.transform.position.x);
        SoundManager.instance.Waterfall(distance, numWaterfall);
	}
}
