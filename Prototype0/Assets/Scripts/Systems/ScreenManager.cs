using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour {

    public BaseClassScreen[] screens;
    public int currentScreen;
    public int defaultScreen;

    public static ScreenManager instance;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        else if (instance != this)
        {
            // Then destroy this.This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    public BaseClassScreen CurrentScreen(int value)
    {
        return screens[value];
    }

    public void OneScreenOnly(int value)
    {
        var length = screens.Length;

        for (int i  = 0 ; i < length; ++i){

            var screen = screens[i];
            if(i == value)
            {
                screen.OpenWindow();
            }
            else if (screen.gameObject.activeSelf)
            {
                screen.CloseWindow();
            }
        }
    }

    internal void InitWindows()
    {
        foreach (BaseClassScreen screen in screens)
        {
            screen.CloseWindow();
        }

        screens[0].OpenWindow();
        screens[2].OpenWindow();
    }

    public BaseClassScreen Open(int value)
    {
        if(value < 0 || value >= screens.Length)
        {
            return null;
        }

        currentScreen = value;
        OneScreenOnly(currentScreen);

        return CurrentScreen(currentScreen);
    }

    private void Start()
    {
        BaseClassScreen.manager = this;
    }

    public void CloseAll()
    {
        foreach(var screen in screens)
        {
            screen.CloseWindow();
        }
    }

    public void ShowGameOver()
    {
        foreach( BaseClassScreen screen in screens)
        {
            screen.CloseWindow();
        }

        screens[3].OpenWindow();
    }


    public void ShowFinishLevel()
    {
        foreach (BaseClassScreen screen in screens)
        {
            screen.CloseWindow();
        }

        screens[1].OpenWindow();
    }



}
