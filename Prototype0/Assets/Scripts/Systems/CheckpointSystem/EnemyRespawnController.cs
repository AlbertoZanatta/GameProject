using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnController : RespawnController {

    public EnemyState enemy;

    public override void OnRespawn()
    {
        base.OnRespawn();
        enemy.Health.Refill();
        enemy.characterRenderer.color = Color.white;
        enemy.ResetBehaviour();
    }


}
