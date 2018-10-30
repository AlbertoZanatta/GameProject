using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {
    public Animator animator;

    private int healthPoints = 4;
    private Collider2D collider;
    

	// Use this for initialization
	void Start () {
        collider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void TakeDamage(int damage)
    {
        Debug.Log("Tree attacked!");
        healthPoints-= damage;
        if (healthPoints <= 0)
            this.gameObject.SetActive(false);
    }


}
