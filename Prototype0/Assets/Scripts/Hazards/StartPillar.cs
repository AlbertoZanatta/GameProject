using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPillar : MonoBehaviour {

    public MovingPillar pillar;

    private void OnDisable()
    {
        pillar.enabled = true;
    }
}
