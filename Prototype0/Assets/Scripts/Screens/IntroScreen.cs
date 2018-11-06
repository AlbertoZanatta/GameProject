using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScreen : BaseClassScreen {

    public Button continueButton;

    public override void OpenWindow()
    {
        bool isAbleToContinue = false;

        continueButton.gameObject.SetActive(isAbleToContinue);

        if (continueButton.gameObject.activeSelf)
        {
            OptionOne = continueButton.gameObject;
        }

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




    
}
