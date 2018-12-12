using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour {

    Vector3 currentCheckpoint;
    int playerHealth;

    List<RespawnController> respawnables = new List<RespawnController>();
    public PlayerRespawnController playerRespawn;
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
        PlayerController.Instance.playerDead += Instance_playerDead;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        PlayerController.Instance.playerDead -= Instance_playerDead;
    }

    private void Instance_playerDead(object sender, PlayerDeadArgs e)
    {
        if(GameController.instance.GetLives() > 1) //Respawn only if player has lives left
        {
            foreach (RespawnController respawnable in respawnables)
            {
                if(respawnable != null)
                {
                    respawnable.OnRespawn();
                }
                
            }

            playerRespawn.OnRespawn();
        }   
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
        RespawnController[] found = FindObjectsOfType<RespawnController>();
        respawnables.Clear();
        foreach (RespawnController respawnable in found)
        {

            if (!respawnable.CompareTag("Player"))
            {
                if (GameController.instance.levelForward)
                {
                    if (respawnable.GetPosition().x >= currentCheckpoint.x || respawnable.gameObject.activeSelf)
                    {
                        respawnables.Add(respawnable);
                    }
                }
                else
                {
                    if (respawnable.GetPosition().x <= currentCheckpoint.x || respawnable.gameObject.activeSelf)
                    {
                        respawnables.Add(respawnable);
                    }
                }
            }
               
        }


    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        playerRespawn = PlayerController.Instance.GetComponent<PlayerRespawnController>();

        //Update list of respawnables
        respawnables.Clear();
        RespawnController[] found = FindObjectsOfType<RespawnController>();
        //Remove the player from the list of respawnables
        foreach(RespawnController respawnable in found)
        {
            if(!respawnable.CompareTag("Player"))
            {
                respawnables.Add(respawnable);
            }
        }
    }



}
