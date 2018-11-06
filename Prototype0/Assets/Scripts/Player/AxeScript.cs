using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour {

    public Weapon weapon = new Weapon(1, 0, Weapon.WeaponType.Axe);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hit = collision.gameObject;
        Debug.Log("Axe hit something! ");

        Damageable damageable = hit.GetComponent<Damageable>();
        if(damageable != null)
        {
            damageable.Hit(weapon);
        }
    }
}
