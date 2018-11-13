using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public bool hasFlag = false;
    public bool facingRight = true;
    int killedEnemies = 0;
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
