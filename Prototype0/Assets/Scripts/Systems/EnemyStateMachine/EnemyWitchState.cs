using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWitchState : EnemyState {

    public GameObject fireballPrefab;
    public float keepDistance = 6f;
    public LootDrop loot;

    public void ThrowFireball()
    {
        Throw(fireballPrefab, spawnPoint.position);
    }
    
    public override void Die()
    {
        /*if (loot != null)
        {
            loot.Drop(transform.position);
        }*/
        base.Die();
    }




}
