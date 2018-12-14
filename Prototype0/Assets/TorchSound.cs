using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TorchSound : MonoBehaviour {
    private static int numTorches;
   [SerializeField] private int numTorch;

    private void Start()
    {
        numTorches++;
        numTorch = numTorches;

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene arg0, LoadSceneMode arg1)
    {
        numTorches = 0;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    // Update is called once per frame
    void Update () {
        float playerDist = Mathf.Abs(transform.position.x -  PlayerController.Instance.transform.position.x);
        SoundManager.instance.Torch(playerDist, numTorch);
	}
}
