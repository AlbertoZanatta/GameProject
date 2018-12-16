using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour { 
    public Text livesText;
    public Text pointsText;
    public GameObject player;

    public bool hasFlag = false;
    public bool facingRight = true;
    public bool levelForward;
    int killedEnemies = 0;

    int maxLives = 3;
    int lives = 5;
    float elapsedTime = 0;

  
    int coinsCollected;
    int points;
    [SerializeField] int pointsForLife = 10; 
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
            if(ScreenManager.instance.CheckScreen())
            {
                ScreenManager.instance.ShowPauseMenu();
            }
            
        }
        elapsedTime += Time.deltaTime;
    }

    private void PauseGame()
    {
        throw new NotImplementedException();
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        lives = maxLives;
        livesText.text = lives.ToString();
        //Debug.Log("Lives set to : " + lives);

        if (scene.name.Contains("Back"))
        {
            levelForward = false;
        }
        else
        {
            coinsCollected = 0;
            points = 0;
            elapsedTime = 0;
            pointsText.text = 0.ToString();
            PlayerController.Instance.inventory.ResetInventory(new List<ItemStack>());
            PlayerController.Instance.Health.Refill();

            levelForward = true;
        }

        GameObject beginLevel = GameObject.FindGameObjectWithTag("LevelBeginning");
        if (beginLevel != null)
        {
            beginLevelPos = beginLevel.transform.position;
            PlayerController.Instance.transform.position = new Vector3(this.beginLevelPos.x, this.beginLevelPos.y, PlayerController.Instance.transform.position.z);
       
         
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
        livesText.text = lives.ToString();
        if(lives == 0)
        {
            
            livesText.text = lives.ToString();
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
        coinsCollected += 1;
        points += 1;
        if(points == pointsForLife)
        {
            lives++;
            livesText.text = lives.ToString();
            points = 0;
        }
        pointsText.text = points.ToString();

    }

    public void FinishLevel()
    {
        ScreenManager.instance.ShowFinishLevel(coinsCollected, elapsedTime);  
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

