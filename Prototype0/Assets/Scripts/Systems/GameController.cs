﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour { 

    [System.Serializable]
    public class LevelSpawn
    {
        public string levelName;
        public Vector3 spawnPosition;
    }

    public Text scoreText;
    public GameObject player;

    public bool hasFlag = false;
    public bool facingRight = true;
    int killedEnemies = 0;
    int score;
    float elapsedTime = 0;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private LevelSpawn[] levelSpawns;

    float timeBetweenSaves = 15f;
    float timeSinceLastSave = 0;

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
        spawnPosition = player.transform.position;
	}
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void Instance_flagCollected(object sender, PlayerFlagArgs e)
    {
        
        spawnPosition = e.catchPosition;
        hasFlag = true;
        string levelToLoad = SceneManager.GetActiveScene().name + "Back";
        SceneManager.LoadScene(levelToLoad);

    }

    private void Instance_playerDead(object sender, PlayerDeadArgs e)
    {
        Debug.Log("PlayerISDead");
        RestartLevel();
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
        //Funzionalità di checkpoint; la posizione del giocatore viene salvata ogni 15 secondi (a patto che si trovi in una posizione sicura)
        timeSinceLastSave += Time.deltaTime;
        if(timeSinceLastSave >= timeBetweenSaves)
        {
            if (PlayerController.Instance.characterRigidbody.velocity == Vector2.zero && PlayerController.Instance.transform.parent == null)//This means the player is outside a platform
            {
                Debug.Log("Player saved position: " + spawnPosition);
                spawnPosition = PlayerController.Instance.transform.position;
                timeSinceLastSave = 0;
            }
        }
	}

    public void FinishLevel()
    {
        //ScreenManager.instance.ShowFinishLevel();  
    }

    public void RestartLevel()
    {
        Debug.Log("Changing position player to " + spawnPosition);
        player.transform.position = spawnPosition;
        CameraFollow.instance.ResetCamera();

    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(!hasFlag)
        {
            foreach(LevelSpawn levelSpawn in levelSpawns)
            {
                if(levelSpawn.levelName == scene.name)
                {
                    player.transform.position = levelSpawn.spawnPosition;
                    CameraFollow.instance.ResetCamera();
                }
            }
        }
        if(hasFlag)
        {
            player.transform.position = spawnPosition;
            CameraFollow.instance.ResetCamera();
        }
        
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

