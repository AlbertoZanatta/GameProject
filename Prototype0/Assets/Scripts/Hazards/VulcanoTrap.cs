using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanoTrap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	 private void OnCollisionEnter2D(Collision2D collision)
    {
		this.gameObject.SetActive(false);
    }
}
