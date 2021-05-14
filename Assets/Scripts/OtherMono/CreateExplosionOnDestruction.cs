using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateExplosionOnDestruction : MonoBehaviour, IDestructable
{
    void IDestructable.OnDestruction(Vector3 deathPosition)
    {
        SpawnManager.Instance.CreateExplosionFX(deathPosition);
    }
}
