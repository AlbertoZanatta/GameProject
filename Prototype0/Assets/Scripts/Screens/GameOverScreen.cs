using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : BaseClassScreen {

    public Text leftStats;
    public Text rightStats;
    public Text leftValues;
    public Text rightValues;
    public Text score;
    public int numOfStats = 6;
    public int statsOnRows = 3;

    private int currentStats = 0;

    public void ResetStatValues()
    {
    }

    public void PrepareStats(Text stats, Text values)
    {
    }

    public void GenerateStats()
    {
    }

    public override void OpenWindow()
    {
        ResetStatValues();
        base.OpenWindow();
    }

    public override void CloseWindow()
    {
        base.CloseWindow();
        currentStats = 0;
    }

    public void OnContinue()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
    }

}
