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
        GameController.instance.GoToMainMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
