using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
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

    public void InitWindows()
    {
        foreach (BaseClassScreen screen in screens)
        {
            screen.CloseWindow();
        }

        screens[0].OpenWindow();
        screens[2].OpenWindow();
    }

    public bool CheckScreen()
    {
        foreach (BaseClassScreen screen in screens)
        {
            if (screen.ScreenId == "Intro_Screen" || screen.ScreenId == "Gameover_Screen" || screen.ScreenId == "Levelcompleted_Screen" || screen.ScreenId == "ButtonInstructions_Screen")
            {
                if (screen.gameObject.activeSelf)
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public void ShowPauseMenu()
    {
        foreach (BaseClassScreen screen in screens)
        {
            if (screen.ScreenId == "Pause_Screen")
            {
                screen.OpenWindow();
            }
            else
            {
                screen.CloseWindow();
            }
        }
        Time.timeScale = 0f;
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
        foreach (BaseClassScreen screen in screens)
        {
            if (screen.ScreenId == "Gameover_Screen")
            {
                screen.OpenWindow();
            }
            else
            {
                screen.CloseWindow();
            }
        }
    }


    public void ShowFinishLevel(int coinsCollected, float elapsedTime)
    {
        foreach (BaseClassScreen screen in screens)
        {
            if (screen.ScreenId == "Levelcompleted_Screen")
            {
                screen.OpenWindow();
                GameOverScreen gameOverScreen = (GameOverScreen)screen;
                if(gameOverScreen != null)
                {
                    gameOverScreen.SetStats(coinsCollected, elapsedTime);
                }
            }
            else
            {
                screen.CloseWindow();
            }
        }
    }



    public void ShowInstructions()
    {
        foreach (BaseClassScreen screen in screens)
        {
            if (screen.ScreenId == "ButtonInstructions_Screen")
            {
                screen.OpenWindow();
            }
            else
            {
                screen.CloseWindow();
            }
        }
    }


    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    { 
        string sceneName = scene.name;
        switch(sceneName)
        {
            case "MainMenu":
                ShowMainMenuScreens();
                break;
            default:
                ShowLevelScreens();
                break;
        }

    }

    public void ShowLevelScreens()
    {
        foreach (BaseClassScreen screen in screens)
        {
            if (screen.ScreenId == "HUD_Screen" || screen.ScreenId == "Inventory_Screen")
            {
                screen.OpenWindow();
            }
            else
            {
                screen.CloseWindow();
            }
        }

    }

    public void ShowMainMenuScreens()
    {
        foreach (BaseClassScreen screen in screens)
        {
            if (screen.ScreenId == "Intro_Screen")
            { 
                screen.OpenWindow();
            }
            else
            {
                screen.CloseWindow();
            }
        }
    }
}
