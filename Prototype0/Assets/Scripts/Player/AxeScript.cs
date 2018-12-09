using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour {

    public Weapon weapon = new Weapon(1, 0, WeaponType.Axe);
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;

        Damageable damageable = hit.GetComponent<Damageable>();
        if(damageable != null)
        {
            damageable.Hit(weapon);
            if(collision.gameObject.CompareTag("Shield"))
            {
                SoundManager.instance.Hit("blocked");
            }
            else
            {
                SoundManager.instance.Hit("hard");
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hit = collision.gameObject;

        Damageable damageable = hit.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.Hit(weapon);

            if (collision.gameObject.CompareTag("Shield"))
            {
                SoundManager.instance.Hit("blocked");
            }
            else
            {
                SoundManager.instance.Hit("hard");
            }
        }
    }
}
