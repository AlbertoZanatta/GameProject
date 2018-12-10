using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyState : EnemyState {
    public LootDrop loot;

    public override void Die()
    {
        if(loot != null)
        {
            loot.Drop(transform.position);
        }
        base.Die();
    }
}
