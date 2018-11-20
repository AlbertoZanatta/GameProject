using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public bool paused;
    public string backToMenu;
    public string exitGame;
    public GameObject pauseMenu;

    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // check if paused game
        if (paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        //use Escape button to resume
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

	}

    public void ResumeGame()
    {
        paused = false;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(backToMenu);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(exitGame);
    }
}
