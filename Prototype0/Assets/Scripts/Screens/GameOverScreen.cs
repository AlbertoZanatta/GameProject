using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : BaseClassScreen {

    public Text coinsCollected;
    public Text elapsedTime;
    public Text totalScore;

    public void ResetStatValues()
    {
        elapsedTime.text = 0.ToString();
        coinsCollected.text = 0.ToString();
        totalScore.text = 0.ToString();
    }
    
    public void SetStats(int coinsCollected, float elapsedTime)
    {
        int minutes = (int)(elapsedTime / 60f);
        int seconds = (int)(elapsedTime % 60f);
        int mseconds = (int)((elapsedTime * 1000) % 1000 );
        this.elapsedTime.text = minutes.ToString() + " min : " + seconds.ToString() + " s : " + mseconds.ToString();
        int score = (int)((1f / elapsedTime) * 100000 * coinsCollected);
        totalScore.text = score.ToString();
        this.coinsCollected.text = coinsCollected.ToString();


    }

    public override void OpenWindow()
    {
        ResetStatValues();
        base.OpenWindow();
    }

    public override void CloseWindow()
    {
        ResetStatValues();
        base.CloseWindow();
       
    }

    public void OnContinue()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
