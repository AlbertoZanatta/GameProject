﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScreen : BaseClassScreen {

    public override void OpenWindow()
    {
        base.OpenWindow();
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
