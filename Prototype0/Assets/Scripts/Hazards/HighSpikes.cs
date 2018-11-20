using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighSpikes : MonoBehaviour {


    public int spikeDamage = 1;
    Weapon weapon = new Weapon(2, 0, Weapon.WeaponType.Trap);


	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.Hit(weapon);
        }
        
        
    }

}
