using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePowerUpOnDestruction : MonoBehaviour, IDestructable
{
    void IDestructable.OnDestruction(Vector3 deathPosition)
    {
        SpawnManager.Instance.CreatePowerUpItem(deathPosition);
    }
}
