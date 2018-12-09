using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{ 

    [SerializeField] private Weapon weapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable character = collision.gameObject.GetComponent<Damageable>();
        if(character != null)
        {
            character.Hit(weapon);
        }
    }
}
