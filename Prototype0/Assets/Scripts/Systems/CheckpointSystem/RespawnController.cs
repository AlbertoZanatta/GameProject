using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour {

    protected Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    public virtual void OnRespawn()
    {
        this.gameObject.SetActive(true);
        transform.position = initialPosition;
    }

    public Vector3 GetPosition()
    {
        return initialPosition;
    }
}
