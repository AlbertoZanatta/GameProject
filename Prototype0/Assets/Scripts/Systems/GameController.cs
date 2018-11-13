using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text scoreText;

    public bool hasFlag = false;
    public bool facingRight = true;
    int killedEnemies = 0;
    int score;
    float elapsedTime = 0;

    public static GameController instance;
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        } else if (instance != this)
        {
            // Then destroy this.This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

    }
    void Start ()
    {
        Time.timeScale = 1f;
        PlayerController.Instance.coinsCollected += Instance_coinsCollected;
	}

    private void Instance_coinsCollected(object sender, CollectedCoinArgs e)
    {
        score += 10;
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update ()
    {
        elapsedTime += Time.deltaTime;
	}

    public void FinishLevel()
    {
        Debug.Log("Level finished");
        Time.timeScale = 1;
        ScreenManager.instance.ShowGameOver();
    }
}
