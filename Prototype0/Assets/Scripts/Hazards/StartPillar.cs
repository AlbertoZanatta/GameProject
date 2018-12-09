using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPillar : MonoBehaviour {

    public MovingPillar pillar;

    private void OnDestroy()
    {
        pillar.enabled = true;
    }
}
