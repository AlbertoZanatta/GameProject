using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfDoor : MeleeEnemyState {
    public OpenDoor openDoor;
	public override void Die()
    {
        openDoor.canOpen = true;
        base.Die();
    }
	
}
