using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour {
    public int numWaterfall;
	// Update is called once per frame
	void Update ()
    {
        float distance = Mathf.Abs(transform.position.x - PlayerController.Instance.transform.position.x);
        SoundManager.instance.Waterfall(distance, numWaterfall);
	}
}
