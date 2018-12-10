using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScreen : BaseClassScreen {

    public Button continueButton;

    public override void OpenWindow()
    {
        base.OpenWindow();
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {

        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        Debug.Log("Options");
    }

    public void Level1()
    {

        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {

        SceneManager.LoadScene("Level2");
    }





}
