using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedDestroyObject : MonoBehaviour, IDestructable
{
    public void OnDestruction(Vector3 deathPosition)
    {
        Destroy(gameObject,0.1f);
    }
}
