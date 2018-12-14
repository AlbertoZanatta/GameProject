using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        ScreenManager.instance.ShowLevelScreens();
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        GameController.instance.GoToMainMenu();
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
