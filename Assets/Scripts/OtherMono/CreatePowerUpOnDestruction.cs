using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePowerUpOnDestruction : MonoBehaviour, IDestructable
{
    private bool wasActivated = false;
    void IDestructable.OnDestruction(Vector3 deathPosition)
    {
        //BugFix to prevent:
        //When multiple projectiles hit and call iDestructables at the same time, multiple PowerUps could spawn
        if (wasActivated) 
            return;

        wasActivated = true;
        SpawnManager.Instance.CreatePowerUpItem(deathPosition);
    }
}
