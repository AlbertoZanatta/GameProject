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
    int lives = 2;
    int score;
    Vector3 beginLevelPos;

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
    private void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            ScreenManager.instance.ShowPauseMenu();
        }
    }

    private void PauseGame()
    {
        throw new NotImplementedException();
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        scoreText.text = lives.ToString();

        if (scene.name.Contains("Back"))
        {
            levelForward = false;
        }
        else
        {
           
           
            levelForward = true;
        }

        GameObject beginLevel = GameObject.FindGameObjectWithTag("LevelBeginning");
        if (beginLevel != null)
        {
            beginLevelPos = beginLevel.transform.position;
            PlayerController.Instance.transform.position = new Vector3(this.beginLevelPos.x, this.beginLevelPos.y, PlayerController.Instance.transform.position.z);
            PlayerController.Instance.Health.Refill();
            PlayerController.Instance.inventory.ResetInventory(new List<ItemStack>());
            if ((levelForward && PlayerController.Instance.transform.localScale.x < 0) || (!levelForward && PlayerController.Instance.transform.localScale.x > 0))
            {
                Vector3 localScale = PlayerController.Instance.transform.localScale;
                PlayerController.Instance.transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
                PlayerController.Instance.FacingRight = !PlayerController.Instance.FacingRight;
            }

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
        lives--;
        scoreText.text = lives.ToString();
        if(lives == 0)
        {
            lives = 2;
            scoreText.text = lives.ToString();
            GameOver();
        }
    }

    private void GameOver()
    {
        ScreenManager.instance.ShowGameOver();
        SoundManager.instance.GameOver("start");
    }

    private void Instance_coinsCollected(object sender, CollectedCoinArgs e)
    {
        score += 10;
        
    }

    public void FinishLevel()
    {
        ScreenManager.instance.ShowFinishLevel();  
        SoundManager.instance.levelComplete("start");
    }

    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
        SoundManager.instance.GameOver("stop");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        SoundManager.instance.levelComplete("stop");
        SoundManager.instance.GameOver("stop");
    }

    public int GetLives()
    {
        return lives;
    }
}

