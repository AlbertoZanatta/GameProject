using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSound : MonoBehaviour {
    private static int numTorches;
    private int numTorch;

    private void Start()
    {
        numTorches++;
        numTorch = numTorches;

    }

    // Update is called once per frame
    void Update () {
        float playerDist = Mathf.Abs(transform.position.x -  PlayerController.Instance.transform.position.x);
        SoundManager.instance.Torch(playerDist, numTorch);
	}
}
