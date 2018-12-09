using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour { 
    public Text scoreText;
    public GameObject player;

    public bool hasFlag = false;
    public bool facingRight = true;
    public bool levelForward;
    int killedEnemies = 0;
    int score;

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

        DontDestroyOnLoad(gameObject);

    }
    void Start ()
    {
        Time.timeScale = 1f;
        PlayerController.Instance.coinsCollected += Instance_coinsCollected;
        PlayerController.Instance.playerDead += Instance_playerDead;
        PlayerController.Instance.flagCollected += Instance_flagCollected;
	}

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Back"))
        {
            levelForward = false;
        }
        else
        {
            levelForward = true;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void Instance_flagCollected(object sender, PlayerFlagArgs e)
    {
        hasFlag = true;
        string levelToLoad = SceneManager.GetActiveScene().name + "Back";
        SceneManager.LoadScene(levelToLoad);

    }

    private void Instance_playerDead(object sender, PlayerDeadArgs e)
    {
        RestartLevel();
    }
    
    private void Instance_coinsCollected(object sender, CollectedCoinArgs e)
    {
        score += 10;
        scoreText.text = score.ToString();
    }

    public void FinishLevel()
    {
        ScreenManager.instance.ShowFinishLevel();  
    }

    public void RestartLevel()
    { 
        CameraFollow.instance.ResetCamera();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

