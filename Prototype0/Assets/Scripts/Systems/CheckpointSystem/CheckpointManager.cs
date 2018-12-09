using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour {

    Vector3 currentCheckpoint;
    int playerHealth;

    RespawnController[] respawnables;
    PlayerRespawnController playerRespawn;
    public static CheckpointManager instance;
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        else if (instance != this)
        {
            // Then destroy this.This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        Debug.Log("CheckpointManager start");
        playerRespawn = PlayerController.Instance.GetComponent<PlayerRespawnController>();        
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        PlayerController.Instance.playerDead += Instance_playerDead;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        PlayerController.Instance.playerDead -= Instance_playerDead;
    }

    private void Instance_playerDead(object sender, PlayerDeadArgs e)
    {
    
        foreach(RespawnController respawnable in respawnables)
        {
            if(GameController.instance.levelForward)
            {
                if (respawnable.GetPosition().x >= currentCheckpoint.x)
                {
                    respawnable.OnRespawn();
                }
            }
            else
            {
                if (respawnable.GetPosition().x <= currentCheckpoint.x)
                {
                    respawnable.OnRespawn();
                }
            }
            
        }

        playerRespawn.OnRespawn();
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        respawnables = FindObjectsOfType<RespawnController>();
    }



}
