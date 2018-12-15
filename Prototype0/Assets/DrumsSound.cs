using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumsSound : MonoBehaviour {
    private static DrumsSound instance;
    public float playerDistance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    private void Update()
    {
        float distance = Mathf.Abs(transform.position.x - PlayerController.Instance.transform.position.x);
        playerDistance = distance;
        SoundManager.instance.Drums(distance);
    }

    private void OnDestroy()
    {
        SoundManager.instance.Drums(float.PositiveInfinity);
    }
}
